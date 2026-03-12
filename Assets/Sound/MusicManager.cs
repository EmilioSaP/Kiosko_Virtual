using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    void Awake()
    {
        // Verificamos si ya existe una instancia de este objeto
        if (instance == null)
        {
            instance = this;
            // Esto hace que el objeto no se destruya al cambiar de escena
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Si ya existe uno, destruimos el duplicado que se crea al volver a la escena inicial
            Destroy(gameObject);
        }
    }
}