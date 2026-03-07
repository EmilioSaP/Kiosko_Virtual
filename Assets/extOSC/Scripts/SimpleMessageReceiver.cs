using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using extOSC;

namespace extOSC.Examples
{
    public class SimpleMessageReceiver : MonoBehaviour
    {
        public OSCReceiver Receiver;
        public RectTransform cursorVisual;

        [Header("Configuracion")]
        public float distanciaParaActivar = 150f; 
        public float tiempoParaClick = 2.0f; 
        
        [Header("Calibracion")]
        public float x; 
        public float y;
        public float multiplicadorY = 1.0f;
        public float offsetY = 0f;

        private float cronometro = 0f;
        private Button botonCercano;
        private Button[] todosLosBotones;

        protected virtual void Start()
        {
            if (Receiver != null)
            {
                Receiver.Bind("/manox", (msg) => { x = msg.Values[0].FloatValue; });
                Receiver.Bind("/manoy", (msg) => { y = msg.Values[0].FloatValue; });
            }
            
            // Usamos la version moderna para evitar el aviso amarillo CS0618 de tu imagen
            todosLosBotones = Object.FindObjectsByType<Button>(FindObjectsSortMode.None);
        }

        private void Update()
        {
            // Movimiento suave del cursor
            Vector2 objetivo;
            if (x == 0 && y == 0) {
                objetivo = new Vector2(-1000, -1000);
            } else {
                float posX = x * Screen.width;
                float posY = (y * Screen.height * multiplicadorY) + offsetY;
                objetivo = new Vector2(posX, posY);
            }

            cursorVisual.position = Vector2.Lerp(cursorVisual.position, objetivo, 0.2f);

            // Verificamos si estamos cerca de algun boton
            VerificarProximidad();
        }

        private void VerificarProximidad()
        {
            Button botonEncontrado = null;

            foreach (Button btn in todosLosBotones)
            {
                if (btn == null || !btn.interactable || !btn.gameObject.activeInHierarchy) continue;

                float distancia = Vector2.Distance(cursorVisual.position, btn.transform.position);

                if (distancia < distanciaParaActivar)
                {
                    botonEncontrado = btn;
                    break;
                }
            }

            if (botonEncontrado != null)
            {
                if (botonEncontrado == botonCercano)
                {
                    cronometro += Time.deltaTime; // Aqui cuenta los 3 segundos
                    
                    // Efecto visual: el cursor crece mientras esperas
                    cursorVisual.localScale = Vector3.one * (1f + (cronometro / tiempoParaClick) * 0.5f);

                    if (cronometro >= tiempoParaClick)
                    {
                        Debug.Log("CLICK EJECUTADO EN: " + botonEncontrado.name);
                        botonEncontrado.onClick.Invoke();
                        cronometro = 0;
                        cursorVisual.localScale = Vector3.one;
                    }
                }
                else
                {
                    botonCercano = botonEncontrado;
                    cronometro = 0;
                    cursorVisual.localScale = Vector3.one;
                }
            }
            else
            {
                botonCercano = null;
                cronometro = 0;
                cursorVisual.localScale = Vector3.one;
            }
        }
    }
}