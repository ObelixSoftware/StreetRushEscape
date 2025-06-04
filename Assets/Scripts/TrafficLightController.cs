using UnityEngine;

public class TrafficLightController : MonoBehaviour
{
    public SpriteRenderer lightRenderer;  // Assign in inspector
    public Sprite redLight;
    public Sprite yellowLight;
    public Sprite greenLight;

    private float timer = 0f;
    public float lightDuration = 3f;
    private int state = 0;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > lightDuration)
        {
            timer = 0f;
            state = (state + 1) % 3;

            switch (state)
            {
                case 0:
                    lightRenderer.sprite = redLight;
                    break;
                case 1:
                    lightRenderer.sprite = greenLight;
                    break;
                case 2:
                    lightRenderer.sprite = yellowLight;
                    break;
            }
        }
    }

    public int GetLightState()
    {
        return state;
    }
}
