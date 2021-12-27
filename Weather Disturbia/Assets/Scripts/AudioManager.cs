using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] playlist;
    public AudioSource audioSource;
    private int musicIndex = 0;

    public AudioMixerGroup soundEffectMixer;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("There is more than one instance of AudioManager in the scene");
            return;
        }
        instance = this;
    }

    void Start()
    {
        audioSource.clip = playlist[0];
        audioSource.Play();
    }

    void Update()
    {
        if(!audioSource.isPlaying)
        {
            PlayNextSong();
        }
    }

    void PlayNextSong()
    {
        musicIndex = (musicIndex + 1) % playlist.Length;
        audioSource.clip = playlist[musicIndex];
        audioSource.Play();
    }

    // Creation of custom PlayClipAtPoint method from Unity (to have control over the volume of the sound effects with the MainMixer)
    // --> Creates a temporary GameObject to play the sound effect before the object is destroyed
    public AudioSource PlayClipAt(AudioClip _clip, Vector3 _pos)
    {
        GameObject tempGO = new GameObject("TempAudio");
        tempGO.transform.position = _pos;
        AudioSource audioSource = tempGO.AddComponent<AudioSource>();
        audioSource.clip = _clip;
        audioSource.outputAudioMixerGroup = soundEffectMixer;
        audioSource.Play();
        Destroy(tempGO, _clip.length);
        return audioSource;
    }
}
