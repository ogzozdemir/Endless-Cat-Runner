using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Space(5), Header("References"), Space(15)]
    private Rigidbody rb;
    private Animator animator;
    [SerializeField] private Joystick joystick;
    
    [Space(5), Header("Player"), Space(15)]
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private ParticleSystem cryParticles;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Start() => StartCoroutine(UIController.instance.GameCountdown());

    private void Update()
    {
        if (!GameController.instance.gameEnded)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Run") && !GameController.instance.runStarted)
                GameController.instance.runStarted = true;

            if (GameController.instance.runStarted)
            {
                if (forwardSpeed < 1000)
                    forwardSpeed += 5f * Time.deltaTime;
            }
        }
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -3f, 3f), -0.7879999f,
            transform.position.z);
        
        if (GameController.instance.runStarted)
        {
            rb.velocity = new Vector3(joystick.Horizontal * horizontalSpeed * Time.fixedDeltaTime, 0f,
                forwardSpeed * Time.fixedDeltaTime);

            transform.rotation = Quaternion.Euler(0, rb.velocity.x * 1.5f, 0);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (GameController.instance.runStarted && !GameController.instance.gameEnded)
        {
            if (other.gameObject.CompareTag("Obstacle"))
            {
                GameController.instance.GameEnded();
                animator.Play("Idle End");
                rb.velocity = Vector3.zero;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                
                cryParticles.Play();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameController.instance.runStarted && !GameController.instance.gameEnded)
        {
            if (other.CompareTag("Collect"))
            {
                GameController.instance.PlayAudioOneShot("Cat Eat");
                GameController.instance.scorePoints += other.GetComponent<FoodController>().pointsToGive;
                UIController.instance.IncreaseScore(other.GetComponent<FoodController>().pointsToGive);
                
                other.gameObject.SetActive(false);
            }
        }
    }
}
