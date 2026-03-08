using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class APIManager : MonoBehaviour
{
    public GameObject showObject;  //visualizer action
    string baseURL = "http://localhost:3000";

    void Start()
    {
        StartCoroutine(GetIdiomas());
    }

    IEnumerator GetIdiomas()
    {
        UnityWebRequest request = UnityWebRequest.Get(baseURL + "/idiomas");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;

            Debug.Log("Respuesta API: " + json);

            IdiomaList lista = JsonUtility.FromJson<IdiomaList>("{\"idiomas\":" + json + "}");

            foreach (Idioma idioma in lista.idiomas)
            {
                Debug.Log("Idioma: " + idioma.nombre + " (" + idioma.codigo + ")");
            }

            if (showObject != null)
             {
                 ConexionExitosa();
             }
        }
        else
        {
            Debug.LogError(request.error);
        }
    }

    public void ConexionExitosa()
    {
        showObject.SetActive(true); // Example of activating the visualizer after fetching data

    }

}

    

[System.Serializable]
public class Idioma
{
    public int idioma_id;
    public string nombre;
    public string codigo;
}

[System.Serializable]
public class IdiomaList
{
    public Idioma[] idiomas;
}