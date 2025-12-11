using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score instance;

    public TextMeshProUGUI puntuacion;
    public TextMeshProUGUI puntuacionFinal;
    public TextMeshProUGUI tiempoFinal;

    private int puntos;

    public int puntosFinales;
    public float tiempoFinalPartida;

    private int bloquesRestantes;
    public MenuStateMachine menus;

    public AudioClip victoriaSound;
    public float victoriaVolume = 2f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
            return;
        }

        puntos = 0;
        bloquesRestantes = 0;
        ActualizarTexto();
    }

    public void SumarPuntos(int total)
    {
        puntos += total;
        ActualizarTexto();
    }

    public void ActualizarTexto()
    {
        if (puntuacion != null)
        {
            puntuacion.text = puntos.ToString();
        }
    }

    public int ObtenerScore()
    {
        return puntos;
    }

    public void GuardarPuntosTotales()
    {
        puntosFinales = puntos;
    }

    public void BloqueCreado()
    {
        bloquesRestantes++;
    }

    public void BloqueDestruido()
    {
        bloquesRestantes--;
        if (bloquesRestantes <= 0)
        {
            Victoria();
        }
    }

    private void Victoria()
    {
        Debug.Log("Has ganado!");

        GuardarPuntosTotales();

        if (Cronometro.instance != null)
        {
            Cronometro.instance.DetenerCronometro();
            tiempoFinalPartida = Cronometro.instance.ObtenerTiempoActual();
        }

        ReproducirSonidoVictoria();
        BallController.DestruirTodasLasBolas();
        BallController.ReinstanciarBola();

        if (puntuacionFinal != null)
        {
            puntuacionFinal.text = puntosFinales.ToString();
        }

        if (tiempoFinal != null)
        {
            int minutos = Mathf.FloorToInt(tiempoFinalPartida / 60f);
            int segundos = Mathf.FloorToInt(tiempoFinalPartida % 60f);
            int centesimas = Mathf.FloorToInt((tiempoFinalPartida * 100f) % 100f);

            tiempoFinal.text = string.Format("{0:00}:{1:00}:{2:00}", minutos, segundos, centesimas);
        }

        if (menus != null && menus.controlMenus != null)
        {
            menus.controlMenus.menuJugar.SetActive(false);
            menus.controlMenus.screenVictoria.SetActive(true);
        }
    }

    public void ReiniciarPuntuacion()
    {
        puntos = 0;
        puntosFinales = 0;
        tiempoFinalPartida = 0f;
        bloquesRestantes = 0;

        ActualizarTexto();
    }

    public void ReproducirSonidoVictoria()
    {
        AudioSource.PlayClipAtPoint(victoriaSound, transform.position, victoriaVolume);
    }

    public void ResetPuntuacion()
    {
        puntos = 0;
    }
}
