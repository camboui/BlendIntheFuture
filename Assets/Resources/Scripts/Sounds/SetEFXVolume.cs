using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetEFXVolume : MonoBehaviour {

	public Slider VolumeBar;

	public void Awake(){
		VolumeBar.value = SoundManager.instance.efxSource.volume;
	}
		
	public void EFXVolume () {
		SoundManager.instance.efxSource.volume = VolumeBar.value;
	}
}
