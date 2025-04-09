using System;
using UnityEngine;

public class Drift : MonoBehaviour
{
    [SerializeField] float accleration = 20f; //전진, 후진 가속도
    [SerializeField] float steering = 3f;     //조향 속도
    [SerializeField] float maxSpeed = 10f;    //최대 속도 제한 
    [SerializeField] float driftFactor = 1.0f;//값이 낮을 수록 더 미끄러진다
    [SerializeField] float slowAcclerationRatio = 0.5f;
    [SerializeField] float boostAcclerationRatio = 1.5f;
    [SerializeField] ParticleSystem smokeLeft;
    [SerializeField] ParticleSystem smokeRight;
    [SerializeField] TrailRenderer LeftTrail;
    [SerializeField] TrailRenderer RightTrail;

    AudioSource audioSource;
    Rigidbody2D rb;

    float defaultAccleration;
    float slowAccleration;
    float boostAccleration;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = rb.GetComponent<AudioSource>();

        defaultAccleration = accleration;
        slowAccleration = accleration * slowAcclerationRatio;
        boostAccleration = accleration * boostAcclerationRatio;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float speed = Vector2.Dot(rb.linearVelocity, transform.up);
        if(speed < maxSpeed)
        {
            rb.AddForce(transform.up * Input.GetAxis("Vertical") * accleration);
        }

        //float turnAmount = Input.GetAxis("Horizontal") * steering * speed * Time.fixedDeltaTime;
        float turnAmount = Input.GetAxis("Horizontal") * steering * Mathf.Clamp(speed / maxSpeed, 0.4f, 1f);
        rb.MoveRotation(rb.rotation - turnAmount);

        //Drift
        Vector2 forwardVelocity= transform.up * Vector2.Dot(rb.linearVelocity, transform.up);
        Vector2 sideVelocity = transform.right * Vector2.Dot(rb.linearVelocity, transform.right);
        rb.linearVelocity = forwardVelocity + (sideVelocity * driftFactor);
    }

    private void Update()
    {
        
        float sidewayVelocity = Vector2.Dot(rb.linearVelocity, transform.right);
        bool isDrifting = rb.linearVelocity.magnitude > 2f && Mathf.Abs(sidewayVelocity) > 1f;
        if(isDrifting)
        {
            if(!smokeLeft.isPlaying) smokeLeft.Play();
            if(!smokeRight.isPlaying) smokeRight.Play();
            if(!audioSource.isPlaying) audioSource.Play();
        }
        else
        {
            if(smokeLeft.isPlaying) smokeLeft.Stop();
            if(smokeRight.isPlaying) smokeRight.Stop();
            if(audioSource.isPlaying) audioSource.Stop();
        }
        LeftTrail.emitting = isDrifting;
        RightTrail.emitting = isDrifting;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Boost"))
        {
            accleration = boostAccleration;
            Debug.Log("부스트!!!!!");

            Invoke(("ResetAccleration"), 5f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        accleration = slowAccleration;
        Invoke(("ResetAccleration"), 2f);
    }

    void ResetAccleration()
    {
        accleration = defaultAccleration;
    }
}
