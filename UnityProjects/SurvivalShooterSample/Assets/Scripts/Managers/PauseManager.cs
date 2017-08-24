using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseManager : MonoBehaviour
{
	Canvas canvas;
    private FMODUnity.StudioEventEmitter pausedSnapshot;
    
    public void SetMusicValue(float value)
    {
        FMODUnity.RuntimeManager.GetBus("bus:/Music").setVolume(value);
    }

    public void SetSoundValue(float value)
    {
        FMODUnity.RuntimeManager.GetBus("bus:/SFX").setVolume(value);
    }

    public void ToggleSound(bool enabled)
    {
        FMODUnity.RuntimeManager.GetBus("bus:/").setVolume(enabled ? 1.0f : 0.0f);
    }
	
	void Start()
	{
		canvas = GetComponent<Canvas>();
        pausedSnapshot = GetComponent<FMODUnity.StudioEventEmitter>();
	}
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Pause();
		}
	}
	
	public void Pause()
	{
	    canvas.enabled = !canvas.enabled;
		Time.timeScale = Time.timeScale == 0 ? 1 : 0;
		Lowpass ();
		
	}
	
	void Lowpass()
	{
		if (Time.timeScale == 0)
		{
		    pausedSnapshot.Play();
		}
		else
		{
		    pausedSnapshot.Stop();
		}
	}
	
	public void Quit()
	{
		#if UNITY_EDITOR 
		EditorApplication.isPlaying = false;
		#else 
		Application.Quit();
		#endif
	}
}
