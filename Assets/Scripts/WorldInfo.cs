using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldInfo : MonoBehaviour
{
    private static WorldInfo instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;  // Store this instance
            DontDestroyOnLoad(gameObject);  // Make it persist
        }
        else
        {
            Destroy(gameObject);  // Prevent duplicates
        }
    }
    public int kioscoID = 1;
    public int idiomaID = 2;
    public bool completada = false;
    public float secondsElapsed = 0f;
    public string review = "";

    public int edad = 0;
    public string genero = "";
    public string nombre = "";
    public string telefono = "";
    public string correo = "";

    public List<int> consultedIDs = new List<int>(); //list of destino_id INT,
    public List<int> anunciosIDs = new List<int>(); //list of anuncio_id INT,
    /*
    "kiosco_id": 1,
    "idioma_id": 2,
    "completada": true,
    "duracion": 180,
    "review": "Muy buena experiencia",

    "edad": 25,
    "genero": "M",

    "nombre": "Fernando Rodriguez",
    "telefono": "0123456789",
    "correo": "juan@gmail.com",

    "destinos": [2,5,7],
    "anuncios": [1,4]
    */

    // - - - - - - - - - - - - - TABLE SESION - - - - - - - - - - - - - 
    //public int kioscoID = 0;      //non-changeable value 
    // sesionID (auto incremental)
    //fecha_inicio = System.DateTime.Now; //session date and time
    //fecha_fin TIMESTAMP
    //public int idioma_id;          //language id
    //public bool completada = false;
    //public float duracion;          //duration in seconds
    //public string review;

    // - - - - - - - - - - - - - TABLE USUARIO - - - - - - - - - - - - - 

    // usuarioID (auto incremental)
    // sesionID (auto incremental; must match from above)   //FOREIGN KEY(sesion_id) REFERENCES Sesion(sesion_id)
    //fecha_registro TIMESTAMP
    // public int edad;
    //public string genero;

    // - - - - - - - - - - - - - TABLE Usuario_Datos_Privados - - - - - - - - - - - - - 
    // usuario_priv_id (auto incremental)
    // usuarioID (auto incremental; must match from above)   //FOREIGN KEY(usuario_id) REFERENCES Usuario(usuario_id)
    //public string nombre;
    //public string telefono;
    //public string correo;

    //public bool sesionterminada = false; //has ever finished the tutorial  < - - -
    //public int idiomaID; //language
    //public float secondsElapsed = 0f;          //time elapsed in seconds

    // - - - - - - - - - - - - - TABLE Consulta_Destino - - - - - - - - - - - - - 
    // consulta_id (auto incremental)
    // sesionID (auto incremental; must match from above)   //FOREIGN KEY(sesion_id) REFERENCES Sesion(sesion_id)
    // destino_id INT,       FOREIGN KEY (destino_id) REFERENCES Destino(destino_id)
    //public List<int> consultedIDs = new List<int>(); //list of destino_id INT,


    // - - - - - - - - - - - - - TABLE Consulta_Anuncio - - - - - - - - - - - - -   
    // consulta_anuncio_id (auto incremental)
    // sesionID (auto incremental; must match from above)   //FOREIGN KEY(sesion_id) REFERENCES Sesion(sesion_id)
    // anuncio_id INT,       FOREIGN KEY (anuncio_id) REFERENCES Anuncio(anuncio_id)
    //public List<int> anunciosIDs = new List<int>(); //list of destino_id INT,

    //NO SUBIR A LA BASE DE DATOS
    public float music = 50f;
    public float volume = 80f;
}



//command F: WorldInfo worldInfo = FindObjectOfType<WorldInfo>();
