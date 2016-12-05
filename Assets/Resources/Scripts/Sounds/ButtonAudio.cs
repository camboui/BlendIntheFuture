using UnityEngine;
using System.Collections;

public class ButtonAudio : MonoBehaviour {

	public AudioClip highlight;
	public AudioClip close;
	public AudioClip load;
	public AudioClip quit;
	public AudioClip pitch;

	public void HighlightSound(){
		SoundManager.instance.RandomizeSfx (highlight);
	}
	public void CloseSound(){
		SoundManager.instance.RandomizeSfx (close);
	}

	public void QuitSound(){
		SoundManager.instance.RandomizeSfx (quit);
	}

	public void LoadSound(){
		SoundManager.instance.RandomizeSfx (load);
	}

	public void PitchSound(){
		SoundManager.instance.RandomizeSfx (pitch);
	}

}
