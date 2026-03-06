using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CambiarTexto : MonoBehaviour
{
    public TextMeshProUGUI texto; // tu texto en el canvas
    public List<string> listaTextos; // lista de textos que quieres mostrar

    public float tiempoCambio = 10f;

    private int indiceActual = 0;

    void Start()
    {
        StartCoroutine(CambiarTextoCoroutine());
    }

    IEnumerator CambiarTextoCoroutine()
    {
        while (true)
        {
            texto.text = listaTextos[indiceActual];

            indiceActual++;

            if (indiceActual >= listaTextos.Count)
            {
                indiceActual = 0; // vuelve a empezar
            }

            yield return new WaitForSeconds(tiempoCambio);
        }
    }
}