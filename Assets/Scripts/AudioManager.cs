using Karma;
using UnityEngine;

namespace GGJ2025
{
    public class AudioManager : Singleton<AudioManager>
    {
        public AudioSource musicSource;
        public AudioSource sfxSource;
        
        public AudioClip mainMenuMusic;
        public AudioClip winMusic;
        
        public void PlayMusic()
        {
            musicSource.clip = mainMenuMusic;
            musicSource.Play();
        }
        
        public void PlayWinMusic()
        {
            musicSource.clip = winMusic;
            musicSource.Play();
        }
        
        public void PlaySFX(AudioClip clip)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}