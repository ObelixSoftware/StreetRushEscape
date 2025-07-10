using UnityEngine;

public class ExplotionEffect : MonoBehaviour
{
    public AudioClip explosionSound;

    void Start()
    {
        AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        Destroy(gameObject, 1f); // Remove explosion after 1 second
    }
}
