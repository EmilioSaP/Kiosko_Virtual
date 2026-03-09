using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class APIManager : MonoBehaviour
{

    string baseURL = "http://localhost:3000";

    public void EnviarSesion()
    {
        StartCoroutine(PostSesion());
    }

    IEnumerator PostSesion()
    {

        WorldInfo w = FindObjectOfType<WorldInfo>();

        SesionData data = new SesionData();

        data.kiosco_id = w.kioscoID;
        data.idioma_id = w.idiomaID;
        data.completada = w.completada;
        data.duracion = w.secondsElapsed;
        data.review = w.review;

        data.edad = w.edad;
        data.genero = w.genero;

        data.nombre = w.nombre;
        data.telefono = w.telefono;
        data.correo = w.correo;

        data.destinos = w.consultedIDs.ToArray();
        data.anuncios = new int[0]; // si luego agregan anuncios consultados

        string json = JsonUtility.ToJson(data);

        UnityWebRequest request = new UnityWebRequest(baseURL + "/sesion-completa", "POST");

        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Sesion enviada correctamente: " + request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Error enviando sesión: " + request.error);
        }

    }
}

[System.Serializable]
public class SesionData
{

    public int kiosco_id;
    public int idioma_id;
    public bool completada;
    public float duracion;
    public string review;

    public int edad;
    public string genero;

    public string nombre;
    public string telefono;
    public string correo;

    public int[] destinos;
    public int[] anuncios;
}