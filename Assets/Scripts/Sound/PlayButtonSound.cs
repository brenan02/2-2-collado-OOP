using UnityEngine;

public class PlayButtonSound : MonoBehaviour
{
    public string soundName = "Click";

    public void PlaySound()
    {
        if (SoundManager.Instance != null)
            SoundManager.Instance.PlaySound2D(soundName);
    }
} 