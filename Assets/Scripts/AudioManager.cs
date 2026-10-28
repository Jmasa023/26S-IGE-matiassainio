using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioClip gameplayMusic;
    public AudioClip gameOverMusic;
    public AudioClip explosionSfx;
    public AudioClip playerHitSfx;
    public AudioClip buttonClickSfx;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayGameplayMusic()
    {
        musicSource.Stop();
        musicSource.clip = gameplayMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayGameOverMusic()
    {
        musicSource.Stop();
        musicSource.clip = gameOverMusic;
        musicSource.loop = false;
        musicSource.Play();
    }

    public void PlayExplosionSfx()
    {
        sfxSource.PlayOneShot(explosionSfx);
    }

    public void PlayPlayerHitSfx()
    {
        sfxSource.PlayOneShot(playerHitSfx);
    }

    public void PlayButtonClickSfx()
    {
        sfxSource.PlayOneShot(buttonClickSfx);
    }
}