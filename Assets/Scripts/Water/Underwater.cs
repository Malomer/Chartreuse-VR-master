using UnityEngine;
using System.Collections;

public class Underwater : MonoBehaviour {

	//This script enables underwater effects. Attach to main camera.

	//Define variable
	public int underwaterLevel = 7;
	public Camera mainCamera;
	//The scene's default fog settings
	private bool defaultFog;
	private Color defaultFogColor;
	private float defaultFogDensity;
	private Material defaultSkybox;
	private Material noSkybox;

	void Start () {
		//Set the background color
		mainCamera.backgroundColor = new Color(0, 0.1f, 0.4f, 1);
		defaultFog = RenderSettings.fog;
		defaultFogColor = RenderSettings.fogColor;
		defaultFogDensity = RenderSettings.fogDensity;
		defaultSkybox = RenderSettings.skybox;
	}

	void Update () {
		if (transform.position.y < underwaterLevel)
		{
			RenderSettings.fog = true;
			RenderSettings.fogColor = new Color(0, 0.1f, 0.4f, 0.6f);
			RenderSettings.fogDensity = 0.04f;
			RenderSettings.skybox = noSkybox;
		}
		else
		{
			RenderSettings.fog = defaultFog;
			RenderSettings.fogColor = defaultFogColor;
			RenderSettings.fogDensity = defaultFogDensity;
			RenderSettings.skybox = defaultSkybox;
		}
	}
}