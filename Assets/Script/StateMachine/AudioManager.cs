using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)] public float volume = 1f;
    //[Range(.1f, 3f)] public float pitch = 1f;
    public bool loop = false;

    [HideInInspector] public AudioSource source;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public Sound[] sounds;

    private void Awake()
    {
        Debug.Log("AudioManager Awake!");

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            //s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void PlayMusic(string name)
    {
        Debug.Log($"🎵 [AudioManager] Yêu cầu phát nhạc: {name}");

        foreach (Sound s in sounds)
        {
            Debug.Log($"🔍 Kiểm tra sound: {s.name}");
            if (s.name == name)
            {
                StopAllMusic();
                s.source.Play();
                Debug.Log($"✅ Đang phát nhạc: {name}, clip: {s.clip}");
                return;
            }
        }

        Debug.LogWarning("❌ Không tìm thấy sound có tên: " + name);
    }



    public void PlaySFX(string name)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
            {
                s.source.PlayOneShot(s.clip);
                return;
            }
        }
    }

    private void StopAllMusic()
    {
        foreach (Sound s in sounds)
        {
            if (s.source.isPlaying) s.source.Stop();
        }
    }
}
