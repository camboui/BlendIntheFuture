using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetMusicVolume : MonoBehaviour {

	public Slider VolumeBar;

	public void Awake(){
		VolumeBar.value = SoundManager.instance.musicSource.volume;
	}

	public void MusicVolume () {
		SoundManager.instance.musicSource.volume = VolumeBar.value;
	}
}
