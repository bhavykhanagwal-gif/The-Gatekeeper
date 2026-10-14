using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class gun : MonoBehaviour
{
 [SerializeField] private anglecalculator anglecalcalculator;
  [SerializeField] private GameObject Bullets;
 [SerializeField]  List<GameObject> bulletList = new List<GameObject>();
 [SerializeField] private float bulletSpeed = 75f;
 [SerializeField] private float firerate = 0.1f;
 [SerializeField] private audiomanager audioManager;
 void destroybullet()
  {
    if (bulletList.Count == 0) return;
    GameObject obj = bulletList[0];
    bulletList.RemoveAt(0);
    Destroy(obj);
    Debug.Log("Bullet destroyed");
  }

  void shotsfired()
  {
    GameObject newObj = Instantiate(Bullets, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
    audioManager.playsfx(audioManager.Gunshot);
          bulletList.Add(newObj);
          
          Rigidbody2D rb = newObj.GetComponent<Rigidbody2D>();
           float angle = Mathf.Atan2(anglecalcalculator.sin, anglecalcalculator.cos) * Mathf.Rad2Deg;
           transform.rotation = Quaternion.Euler(0f, 0f, angle);

            rb.AddForce(new Vector2(anglecalcalculator.cos, anglecalcalculator.sin) * bulletSpeed, ForceMode2D.Impulse);
           Invoke(nameof(destroybullet), 2f);
  }


 private void shoot()
    {
         if (Mouse.current.leftButton.isPressed)
        {
            shotsfired();
        }

    }

private void Start()
    {
        InvokeRepeating(nameof(shoot), firerate, firerate);
    }
    void Update()
    {
    float angle = Mathf.Atan2(anglecalcalculator.sin, anglecalcalculator.cos) * Mathf.Rad2Deg;
    transform.rotation = Quaternion.Euler(0f, 0f, angle);

    } 

 



}
