using UnityEngine;

public class Delivery : MonoBehaviour
{

    bool hasChicken;
    //[SerializeField] float DestroyChicken = 1f;
    [SerializeField] Color noChickenColor = new Color(1, 1, 1, 1);
    [SerializeField] Color hasChickenColor = new Color(0.3f, 1, 0, 1);
    SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("아야!" + collision.gameObject, gameObject);
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Chicken") && !hasChicken)
        {
            Debug.Log("치킨 픽업됨");
            hasChicken = true;
            Destroy(collision.gameObject, 0.5f);
            spriteRenderer.color = hasChickenColor;
        }
        if (collision.gameObject.CompareTag("Customer") && hasChicken)
        {
            Debug.Log("치킨 배달 됨");
            hasChicken = false;
            spriteRenderer.color = noChickenColor;
        }
    }
}
