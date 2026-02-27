using UnityEngine;
using UnityEngine.SceneManagement;

public class Back : MonoBehaviour
{
     public void ReturnButton (string nombre)
	{
	SceneManager.LoadScene (nombre);
	}
}
