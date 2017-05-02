using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;

    public AudioClip StartClip;
    public AudioClip GameClip;
    public AudioClip EndClip;

    private AudioSource music;

    void Start () {
		if (instance != null && instance != this) {
			Destroy (gameObject);
			print ("Duplicate music player self-destructing!");
        }
        else
        {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
            // Start first music
            music = GetComponent<AudioSource>();
            music.clip = StartClip;
            music.loop = true;
            music.Play();
        }
    }

    void OnLevelWasLoaded(int level)
    {
        if(music == null)
            music = GetComponent<AudioSource>();
        music.Stop();
        switch(level)
        {
            case 0:
                music.clip = StartClip;
                break;
            case 1:
                music.clip = GameClip;
                break;
            case 2:
                music.clip = EndClip;
                break;
        }
        music.loop = true;
        music.Play();
    }
}
