using UnityEngine;

public class audiomanager : MonoBehaviour
{
    public static audiomanager Instance { get; private set; }
    [SerializeField] public AudioSource audioSource;
    [SerializeField] public AudioSource audioSFX;

    public AudioClip Gunshot;
    public AudioClip jumpSound;
    public AudioClip enemyDeathSound    ;
    public AudioClip BackgroundMusic;
    public AudioClip platformDestroySound;
    public AudioClip boomSound;
    void Awake()
{
    Instance = this;
    Debug.Log("audiomanager Awake ran, Instance set");
}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource.clip = BackgroundMusic;
        audioSource.Play();   
    }

    public void playsfx(AudioClip clip)
    {
        audioSFX.PlayOneShot(clip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
