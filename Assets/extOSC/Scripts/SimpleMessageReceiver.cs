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
        public float distanciaParaActivar = 300f; // Ya lo subimos a 300 como pediste
        public float tiempoParaClick = 3.0f; 
        
        [Header("Calibración")]
        public float x; public float y;
        public float multiplicadorY = 1.0f;
        public float offsetY = 0f;

        private float cronometro = 0f;
        private Selectable objetoCercano;
        private Button[] todosLosBotones;
        private Slider[] todosLosSliders;
        private Scrollbar[] todosLosScrollbars; // NUEVO: Lista de scrollbars

        protected virtual void Start()
        {
            if (Receiver != null)
            {
                Receiver.Bind("/manox", (msg) => { x = msg.Values[0].FloatValue; });
                Receiver.Bind("/manoy", (msg) => { y = msg.Values[0].FloatValue; });
                
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
            // NUEVO: Detectar las scrollbars de la escena
            todosLosScrollbars = Object.FindObjectsByType<Scrollbar>(FindObjectsSortMode.None);
        }

        private void Update()
        {
            // Mover cursor (Ajuste de límites básico para que no se pierda)
            float posX = Mathf.Clamp(x * Screen.width, 0, Screen.width);
            float posY = Mathf.Clamp((y * Screen.height * multiplicadorY) + offsetY, 0, Screen.height);
            
            Vector2 objetivo = (x == 0 && y == 0) ? new Vector2(-1000, -1000) : new Vector2(posX, posY);
            cursorVisual.position = Vector2.Lerp(cursorVisual.position, objetivo, 0.2f);

            ManejarInteraccion();
        }

        private void ManejarInteraccion()
        {
            Selectable encontrado = null;

            // 1. NUEVA PRIORIDAD: Scrollbars (Verticales)
            foreach (Scrollbar scb in todosLosScrollbars)
            {
                if (scb == null || !scb.interactable || !scb.gameObject.activeInHierarchy) continue;
                if (Vector2.Distance(cursorVisual.position, scb.transform.position) < distanciaParaActivar) {
                    ControlarScrollbarVertical(scb);
                    encontrado = scb;
                    break;
                }
            }

            // 2. Prioridad Sliders (Settings)
            if (encontrado == null) {
                foreach (Slider sld in todosLosSliders)
                {
                    if (sld == null || !sld.interactable || !sld.gameObject.activeInHierarchy) continue;
                    if (Vector2.Distance(cursorVisual.position, sld.transform.position) < distanciaParaActivar) {
                        ControlarSlider(sld);
                        encontrado = sld;
                        break;
                    }
                }
            }

            // 3. Botones
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

        // MÉTODO NUEVO PARA LA BARRA DE LA DERECHA
        private void ControlarScrollbarVertical(Scrollbar s) {
            RectTransform scrollRect = s.GetComponent<RectTransform>();
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(scrollRect, cursorVisual.position, null, out localPoint);
            
            // Calculamos la posición Y normalizada
            float normalizedValue = Mathf.Clamp01((localPoint.y + scrollRect.rect.height * 0.5f) / scrollRect.rect.height);
            
            // CORRECCIÓN DE DIRECCIÓN:
            // Si subes la mano y baja el scroll, usa: s.value = 1 - normalizedValue;
            // Si subes la mano y sube el scroll, usa: s.value = normalizedValue;
            s.value = Mathf.Lerp(s.value, normalizedValue, Time.deltaTime * 10f);
            
            cronometro = 0; 
            cursorVisual.localScale = Vector3.one;
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