using UnityEngine;

public class TrafficLightController : MonoBehaviour
{
    public SpriteRenderer lightRenderer; // Assign this in the Inspector
    public Sprite redLight;
    public Sprite yellowLight;
    public Sprite greenLight;
    
    private int state = 0; // 0 = Red, 1 = Green, 2 = Yellow

    public void SetLight(int newState)
    {
        state = newState;

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

    public int GetLightState()
    {
        return state;
    }
}
