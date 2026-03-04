using UnityEngine;
using System.Collections; 
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
	private CrossfadeScript crossfadeScript;
	public float waitTime = 0.5f;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		crossfadeScript = FindAnyObjectByType<CrossfadeScript>();
	}
	public void GoToScene(string nombre)
	{
		crossfadeScript.FadeOut();
		StartCoroutine(DelayScene(nombre));
	}
	
	IEnumerator DelayScene(string _nombre)
    {
        yield return new WaitForSeconds(waitTime);
		yield return new WaitForSeconds(waitTime/2);
		SceneManager.LoadScene(_nombre);
    }

}
