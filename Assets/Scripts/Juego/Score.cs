using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score instance;

    public TextMeshProUGUI puntuacion;

    private int puntos;

    public static int puntosFinales;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        puntos = 0;
        ActualizarTexto();
    }

    public void SumarPuntos(int total)
    {
        puntos += total;
        ActualizarTexto();
    }

    public void ResetearScore()
    {
        puntos = 0;
        ActualizarTexto();
    }

    void ActualizarTexto()
    {
        if(puntuacion != null)
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
}
