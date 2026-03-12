using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWorldInfo : MonoBehaviour
{
    private WorldInfo w;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        w = FindObjectOfType<WorldInfo>();
    }

    // Update is called once per frame
    public void SetLanguage(int _idioma)
    {
        w.idiomaID = _idioma;
    }

    public void Review(string review)
    {
        w.review = review;
    }

    public void BeginTimer()
    {
        w.isRunning = true;
    }

    public void UsuarioInfo(int edad, string genero, string nombre, string telefono, string correo)
    {
        w.edad = edad;
        w.genero = genero;
        w.nombre = nombre;
        w.telefono = telefono;
        w.correo = correo;
    }

    public void AddConsulta(int id)
    {
        w.consultedIDs.Add(id);
    }

    public void AddConsultaAnuncio(int id)
    {
         w.anunciosIDs.Add(id);
    }

    public void ResetWorldInfo()
    {
        w.idiomaID = 1;
        w.completada = false;
        w.secondsElapsed = 0f;
        w.review = "";  //"Buena","Mala","Normal"

        w.edad = 0;
        w.genero = "";
        w.nombre = "";
        w.telefono = "";
        w.correo = "";

        w.consultedIDs = new List<int>(); //list of destino_id INT,
        w.anunciosIDs = new List<int>(); //list of anuncio_id INT,

        w.isRunning = false;
    }
}
