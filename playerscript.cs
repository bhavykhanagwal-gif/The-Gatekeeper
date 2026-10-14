using UnityEngine;
using UnityEngine.InputSystem;


public class player : MonoBehaviour
{
   Rigidbody2D rb;
   [SerializeField] private anglecalculator anglecalcalculator;
   [SerializeField] private ScoreManager scoreManager;
   private Animator anim;
   [SerializeField] float moveSpeed = 10f;
   [SerializeField] float jumpForce = 24f;
   [SerializeField] bool singlejump = false;
   [SerializeField] bool isgrounded = true;
   [SerializeField] float knockback = 100f;
   [SerializeField] float headbutt = 9f;
   [SerializeField] float gameoverspeed = 20f;
   public GameObject explosionPrefab;
   public int kills = 0;
   bool isdead = false;
  // [SerializeField] private audiomanager audioManager;
      void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        
        
    }

    void HandleAnimations()
    {
        bool ismoving = rb.linearVelocity.x != 0f;
        anim.SetBool("ismoving", ismoving);
        anim.SetBool("isgrounded", isgrounded);
    }

   /* void glide()
    {
      if (Keyboard.current.wKey.isPressed && !isgrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -moveSpeed*0.7f);
        }
    }
*/
    void dropdown()
    {
      if (Keyboard.current.sKey.isPressed && !isgrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -moveSpeed*2.3f);
        }
    }

    void Update()
    {
        if (isdead)
        {
            return;
        }
       HandleAnimations();
       if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            if(scoreManager.booms>0)
            {
                audiomanager.Instance.playsfx(audiomanager.Instance.boomSound);
                scoreManager.GoBoom();
                rb.AddForce(new Vector2(anglecalcalculator.cos, anglecalcalculator.sin) * knockback, ForceMode2D.Impulse);
                Debug.Log("BAMN!");
            }        
        }


         if (Keyboard.current.spaceKey.wasPressedThisFrame && !singlejump && isgrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            singlejump = true;
            isgrounded = false;
            audiomanager.Instance.playsfx(audiomanager.Instance.jumpSound);
        } else if (Keyboard.current.spaceKey.wasPressedThisFrame && singlejump && !isgrounded)
        {
            rb.AddForce(Vector2.up * jumpForce * 1.7f, ForceMode2D.Impulse);
            singlejump = false;
            audiomanager.Instance.playsfx(audiomanager.Instance.jumpSound);
            Debug.Log("double jump");
            
        }


         if (Keyboard.current.dKey.isPressed)
        {
            rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
        }

         if (Keyboard.current.aKey.isPressed)
        {
            rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.y);
        }

      //Invoke("glide", 0.1f);
      Invoke("dropdown", 0.1f);
      if(rb.linearVelocity.magnitude>gameoverspeed)
      {
        FindAnyObjectByType<GameManager>()?.GameOver();
        isdead = true;
      }
       
    }



    void OnCollisionEnter2D(Collision2D col)
{
    if (col.gameObject.CompareTag("Platform"))
    {
        isgrounded = true;
        singlejump = false;
        
        
    }
}
void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("enemy"))
        {
        if(rb.linearVelocity.magnitude > headbutt)
        {
            audiomanager.Instance.playsfx(audiomanager.Instance.enemyDeathSound);
            scoreManager.AddKill();
            scoreManager.AddBoom();
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Debug.Log($"Lives Taken: {kills}");
            Destroy(other.gameObject);
        }
        else
        {      
        FindAnyObjectByType<GameManager>()?.GameOver();
        }
        }
    }


}
