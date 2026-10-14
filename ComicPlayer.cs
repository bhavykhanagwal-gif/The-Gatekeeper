using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ComicPlayer : MonoBehaviour
{
    public Image panelImage;        // drag ComicPanel here
    public Sprite[] panels;         // drag your comic images here, in order
    public string gameSceneName = "SampleScene";   // your game scene's name

    int index = 0;

    void Start()
    {
        panelImage.sprite = panels[0];
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame || Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            index++;

            if (index < panels.Length)
                panelImage.sprite = panels[index];
            else
                SceneManager.LoadScene(2);
        }
    }
}