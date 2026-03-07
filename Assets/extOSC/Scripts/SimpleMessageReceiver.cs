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

        [Header("Configuración León")]
        public Animator leonAnimator; 

        [Header("Configuración Interacción")]
        public float distanciaParaActivar = 150f; 
        public float tiempoParaClick = 3.0f; 
        
        [Header("Calibración")]
        public float x; public float y;
        public float multiplicadorY = 1.0f;
        public float offsetY = 0f;

        private float cronometro = 0f;
        private Selectable objetoCercano;
        private Button[] todosLosBotones;
        private Slider[] todosLosSliders;

        protected virtual void Start()
        {
            if (Receiver != null)
            {
                Receiver.Bind("/manox", (msg) => { x = msg.Values[0].FloatValue; });
                Receiver.Bind("/manoy", (msg) => { y = msg.Values[0].FloatValue; });
                
                // NUEVO: Recibe 1 (cerca) o 0 (lejos) de TouchDesigner
                Receiver.Bind("/presencia", (msg) => {
                    if(leonAnimator != null) {
                        bool estaCerca = msg.Values[0].FloatValue > 0.5f;
                        leonAnimator.SetBool("usuarioCerca", estaCerca);
                    }
                });
            }
            ActualizarElementosUI();
        }

        public void ActualizarElementosUI()
        {
            todosLosBotones = Object.FindObjectsByType<Button>(FindObjectsSortMode.None);
            todosLosSliders = Object.FindObjectsByType<Slider>(FindObjectsSortMode.None);
        }

        private void Update()
        {
            // Mover cursor
            Vector2 objetivo = (x == 0 && y == 0) ? new Vector2(-1000, -1000) : new Vector2(x * Screen.width, (y * Screen.height * multiplicadorY) + offsetY);
            cursorVisual.position = Vector2.Lerp(cursorVisual.position, objetivo, 0.2f);

            ManejarInteraccion();
        }

        private void ManejarInteraccion()
        {
            Selectable encontrado = null;

            // 1. Prioridad Sliders (Arregla lo de las barras de settings)
            foreach (Slider sld in todosLosSliders)
            {
                if (sld == null || !sld.interactable || !sld.gameObject.activeInHierarchy) continue;
                if (Vector2.Distance(cursorVisual.position, sld.transform.position) < distanciaParaActivar) {
                    ControlarSlider(sld);
                    encontrado = sld;
                    break;
                }
            }

            // 2. Si no es slider, buscar Botones
            if (encontrado == null)
            {
                foreach (Button btn in todosLosBotones)
                {
                    if (btn == null || !btn.interactable || !btn.gameObject.activeInHierarchy) continue;
                    if (Vector2.Distance(cursorVisual.position, btn.transform.position) < distanciaParaActivar) {
                        encontrado = btn;
                        break;
                    }
                }
                if (encontrado is Button) ManejarTiempoDeClick((Button)encontrado);
                else ResetearEstado();
            }
        }

        private void ControlarSlider(Slider s) {
            RectTransform sliderRect = s.GetComponent<RectTransform>();
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(sliderRect, cursorVisual.position, null, out localPoint);
            float normalizedValue = Mathf.Clamp01((localPoint.x + sliderRect.rect.width * 0.5f) / sliderRect.rect.width);
            s.value = Mathf.Lerp(s.minValue, s.maxValue, normalizedValue);
        }

        private void ManejarTiempoDeClick(Button b) {
            if (b.gameObject == (objetoCercano ? objetoCercano.gameObject : null)) {
                cronometro += Time.deltaTime;
                cursorVisual.localScale = Vector3.one * (1f + (cronometro / tiempoParaClick) * 0.5f);
                if (cronometro >= tiempoParaClick) {
                    b.onClick.Invoke();
                    cronometro = 0;
                }
            } else { objetoCercano = b; cronometro = 0; }
        }

        private void ResetearEstado() {
            objetoCercano = null;
            cronometro = 0;
            cursorVisual.localScale = Vector3.one;
            if (Time.frameCount % 60 == 0) ActualizarElementosUI(); 
        }
    }
}