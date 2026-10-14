using UnityEngine;
using System.Collections.Generic;

public class gamecontrolla : MonoBehaviour
{
    public GameObject PlatformexplosionPrefab;
    [SerializeField] private GameObject enemyPrefab;
     [SerializeField] private GameObject platformPrefab;
     [SerializeField] private GameObject player;
    [SerializeField] private audiomanager audioManager;
     float enemyspawnrate = 3f;
     float difficulty = 1.3f;
     bool candestroyplatform = false;


      [SerializeField]  List<GameObject> platforms = new List<GameObject>();
      

      void destroyplatform()
    {
       GameObject obj = platforms[0];
         platforms.RemoveAt(0);
        Destroy(obj);
    }

    void spawnenemy()
    {
        float cos = Random.Range(-1f, 1f);
        float sin = Random.Range(-1f, 1f);
        Vector3 spawnPosition = new Vector3(player.transform.position.x + 13f*cos, player.transform.position.y + 13f*sin, 0f);
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        newEnemy.GetComponent<enemy>().SetPlayer(player);

        if (enemyspawnrate < 0.5f)
        {
            return;
        }
        else
        {
            enemyspawnrate -= 0.1f * Mathf.Pow(1.43f, difficulty);
             CancelInvoke(nameof(spawnenemy));
            InvokeRepeating(nameof(spawnenemy), enemyspawnrate, enemyspawnrate);
        }
    }

    void spawnplatform()
    {
        float cos;
        int leftorright = Random.Range(0, 2);
        if (leftorright == 0)
        {
            cos = Random.Range(-1f, -0.75f);
        }
        else
        {
            cos = Random.Range(0.75f, 1f);
        }
        float sin = Random.Range(0.5f, 1f);
        Vector3 spawnPosition;
        if(platforms.Count==0)
        {
        spawnPosition = new Vector3(player.transform.position.x + 13f * cos, player.transform.position.y + 7f * sin, 0f);
        
        }
        else
        {
        spawnPosition = new Vector3(player.transform.position.x + 13f * cos, platforms[platforms.Count-1].transform.position.y + platform_distance * sin, 0f);
        }
       
        GameObject platform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        platforms.Add(platform);
        if(candestroyplatform)
        {
            audioManager.playsfx(audioManager.platformDestroySound);
            Instantiate(PlatformexplosionPrefab, platforms[0].transform.position, Quaternion.identity);
            destroyplatform();
        }
    }
[SerializeField] float platform_distance = 11f;  
[SerializeField] float platformspawnrate = 2f;
    void Start()
    {
        Invoke("candestroyplatforms", 6*platformspawnrate);
        Invoke(nameof(candestroyplatforms), 6*platformspawnrate);
        InvokeRepeating(nameof(spawnenemy), 0f, enemyspawnrate);
        InvokeRepeating(nameof(spawnplatform), 0f, platformspawnrate);
    }

    // Update is called once per frame
    void Update()
    {
       

    }

    void candestroyplatforms()
    {
        candestroyplatform = true;
    }
}
