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
    /**/

    public int kioscoID = 0;           //kiosco id

    //public bool sesionterminada = false; //has ever finished the tutorial  < - - -
    public int idiomaID; //language
    public float secondsElapsed = 0f;          //time elapsed in seconds
    
    public List<int> consultedIDs = new List<int>(); //list of consulted destination IDs

    //NO SUBIR A LA BASE DE DATOS
    public float music = 50f;
    public float volume = 80f;
}

//command F: WorldInfo worldInfo = FindObjectOfType<WorldInfo>();
