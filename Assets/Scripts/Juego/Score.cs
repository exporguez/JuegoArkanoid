using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score instance;

    public TextMeshProUGUI puntuacion;

    private int puntos;

    public static int puntosFinales;

    private int bloquesRestantes;
    public MenuStateMachine menus;

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

    public void BloqueCreado()
    {
        bloquesRestantes++;
    }

    public void BloqueDestruido()
    {
        bloquesRestantes--;
        if(bloquesRestantes <= 0)
        {
            Victoria();
        }
    }

    private void Victoria()
    {
        Debug.Log("Has ganado!");
        GuardarPuntosTotales();
        if(menus != null && menus.controlMenus != null)
        {
            menus.controlMenus.menuJugar.SetActive(false);
            menus.controlMenus.screenVictoria.SetActive(true);
        }
    }
}
