using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class APITest : MonoBehaviour
{
    public GameObject gameObject;
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
            Successful();
            string json = request.downloadHandler.text;

            Debug.Log("Respuesta API: " + json);

            IdiomaList lista = JsonUtility.FromJson<IdiomaList>("{\"idiomas\":" + json + "}");

            foreach (Idioma idioma in lista.idiomas)
            {
                Debug.Log("Idioma: " + idioma.nombre + " (" + idioma.codigo + ")");
            }
        }
        else
        {
            Debug.LogError(request.error);
        }
    }
    public void Successful()
    {
        gameObject.SetActive(true);
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