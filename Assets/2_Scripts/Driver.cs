using Unity.VisualScripting;
using UnityEngine;

public class Driver : MonoBehaviour
{
    bool hasChicken = false;    
    [SerializeField] float turnSpeed = 20f;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float slowSpeedRatio = 0.5f;
    [SerializeField] float boostSpeedRatio = 1.5f;
    float slowSpeed;
    float boostSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slowSpeed = moveSpeed * slowSpeedRatio;
        boostSpeed = moveSpeed * boostSpeedRatio;
    }

    // Update is called once per frame
    void Update()
    {     
        float turnAmount = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
        float moveAmount = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Rotate(0, 0, -turnAmount);
        transform.Translate(0, moveAmount, 0);
       
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Boost"))
        {
            moveSpeed = boostSpeed;
            Debug.Log("ºÎ½ºÆ®!!!!!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        moveSpeed = slowSpeed;
    }

}
