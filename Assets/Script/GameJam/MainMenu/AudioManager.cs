using UnityEngine;
using UnityEngine.SceneManagement; // để lắng nghe khi scene load

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)] public float volume = 1f;
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
            s.source.loop = s.loop;
        }

        // Lắng nghe sự kiện load scene
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"[AudioManager] Scene loaded: {scene.name}");
        UpdateMusicForScene(scene.name);
    }

    // 🎵 Chọn nhạc theo tên scene
    public void UpdateMusicForScene(string sceneName)
    {
        StopAllMusic();

        // Dựa theo tên scene, đổi nhạc tương ứng
        if (sceneName == "Map1" || sceneName == "Map2")
        {
            PlayMusic("Music_M1-2");
        }
        else if (sceneName == "Map3" || sceneName == "Map4")
        {
            PlayMusic("Music_M3-4");
        }
        else if (sceneName == "Map5" || sceneName == "Map6")
        {
            PlayMusic("Music_M5-6");
        }
        else if (sceneName == "Map7")
        {
            Debug.Log("No music for Map7");
        }
        else
        {
            // Các scene khác (menu, intro, v.v)
            PlayMusic("menu");
        }
    }

    // ======================
    // Các hàm chơi nhạc / SFX
    // ======================

    public void PlayMusic(string name)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
            {
                StopAllMusic();
                s.source.Play();
                //Debug.Log($"Playing music: {name}");
                return;
            }
        }

        //Debug.LogWarning($"[AudioManager] Music '{name}' not found!");
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

        //Debug.LogWarning($"[AudioManager] SFX '{name}' not found!");
    }

    private void StopAllMusic()
    {
        foreach (Sound s in sounds)
        {
            if (s.source.isPlaying)
                s.source.Stop();
        }
    }
}
