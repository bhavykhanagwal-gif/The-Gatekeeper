using UnityEngine;

public class enemy : MonoBehaviour
{
public GameObject explosionPrefab;
public int boomcounter=0;
[SerializeField] private GameObject player;

SpriteRenderer sr;
  //[SerializeField] private audiomanager audioManager;
  
  private bool dead = false;
     public void SetPlayer(GameObject playerRef)
    {
        player = playerRef;
    }

  void destroyenemy()
    {
        Destroy(gameObject);
        Debug.Log("Enemy destroyed");
    }
    void Start()
    {
    sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 3f * Time.deltaTime);

        }
        if(player.transform.position.x>transform.position.x)
        {
        sr.flipX = false;
        }
        else
        {
        sr.flipX = true;
        }
        
       
    }

  private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.gameObject.CompareTag("bullet"))
    {
        dead = true;

        if (audiomanager.Instance != null)
        {
            audiomanager.Instance.playsfx(audiomanager.Instance.enemyDeathSound);
            player.GetComponent<player>().kills++;
            Debug.Log($"Lives Taken: {player.GetComponent<player>().kills}");

        }
        else
        {
            Debug.LogWarning("audiomanager.Instance is null — is the audiomanager GameObject in the scene and active?");
        }
        
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        FindObjectOfType<ScoreManager>().AddKill();
        FindObjectOfType<ScoreManager>().AddBoom();
        destroyenemy();
       
        
    }
}

}
