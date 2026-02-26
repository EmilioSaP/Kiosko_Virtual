using UnityEngine;
using System.Collections; 
using UnityEngine.SceneManagement;

public class Back : MonoBehaviour
{
	private CrossfadeScript crossfadeScript;
	public float waitTime = 1.0f;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		crossfadeScript = FindAnyObjectByType<CrossfadeScript>();
	}
	public void ReturnButton(string nombre)
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
