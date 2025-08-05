using UnityEngine;

public class PedestrianLightController : MonoBehaviour
{
    public SpriteRenderer lightRenderer;
    public Sprite redSprite;
    public Sprite greenSprite;

    private bool isGreen = false;

    void Start()
    {
        SetLight(false); // Start with red
    }

    public void SetLight(bool green)
    {
        isGreen = green;
        if (lightRenderer != null)
        {
            lightRenderer.sprite = green ? greenSprite : redSprite;
        }
    }

    public bool IsGreen()
    {
        return isGreen;
    }
}
