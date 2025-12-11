using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class Cronometro : MonoBehaviour
{
    public static Cronometro instance;

    public TextMeshProUGUI cronometro;


    public float tiempoTranscurrido = 0f;
    private bool cronometroActivo = false;

    public static float tiempoPartida = 0f;



    bool estaJugando = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ReiniciarCronometro();
    }

    private void Update()
    {
        if (cronometroActivo)
        {
            tiempoTranscurrido += Time.deltaTime;
            ActualizarTiempo(tiempoTranscurrido);
        }
    }
    public void IniciarCronometro()
    {
        cronometroActivo = true;
    }

    public void DetenerCronometro()
    {
        cronometroActivo = false;
    }

    public void ActualizarTiempo(float tiempo)
    {
        if (cronometro == null) return;

        int minutos = Mathf.FloorToInt(tiempo / 60f);
        int segundos = Mathf.FloorToInt(tiempo % 60f);
        int centesimas = Mathf.FloorToInt((tiempo * 100f) % 100f);

        cronometro.text = string.Format("{0:00}:{1:00}:{2:00}", minutos, segundos, centesimas);
    }

    public void ReiniciarCronometro()
    {
        tiempoTranscurrido = 0f;
        cronometroActivo = false;
        ActualizarTiempo(0f);
    }

    public float ObtenerTiempoActual()
    {
        return tiempoTranscurrido;
    }

    public void ResetearCronometro()
    {
        tiempoTranscurrido = 0f;
    }
}
