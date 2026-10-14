using UnityEngine;

public class platform : MonoBehaviour
{
    Vector3 startpos;
    [SerializeField] float speed; 
    [SerializeField] float extremepos = 20f;
    Vector2 movementDirection = Vector2.right;

    void Start()
    {
    startpos = transform.position;
    speed = Random.Range(1f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
    if((transform.position-startpos).magnitude>extremepos)
    {
        movementDirection = -movementDirection;
    }
    transform.Translate(movementDirection * speed * Time.deltaTime);
    }
}
