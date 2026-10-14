using UnityEngine;
using UnityEngine.InputSystem;

public class anglecalculator : MonoBehaviour
{
    float angletick = 0.02f;
    public float cos;
    public float sin;
    void Start()
    {
        InvokeRepeating(nameof(anglecalc), 0f, angletick); 
    }

    // Update is called once per frame
    void anglecalc()
    {
      Vector3 cursorWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
       cursorWorldPos.z = 0f;

       Vector2 displacement = (Vector2)cursorWorldPos - (Vector2)gameObject.transform.position;
       Vector2 displacementUnit = displacement.normalized;

       Vector2 horizontal = Vector2.right; // (1, 0)
       Vector2 vertical = Vector2.up; // (0, 1)

       float cos = Vector2.Dot(horizontal, displacementUnit);
       float sin = Vector2.Dot(vertical, displacementUnit);
       this.cos = cos;
       this.sin = sin;
      // Debug.Log(cos+","+sin);
    }
}
