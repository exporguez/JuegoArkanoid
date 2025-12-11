using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Patrón Singleton para acceso global
    public static GameManager instance;

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

    /// <summary>
    /// Función de utilidad que se usa después de perder una vida (resetea solo la bola y el jugador).
    /// </summary>
    public void ResetPlayerAndBall()
    {
        

        // 1. Resetear la Bola
        BallController bola = FindObjectOfType<BallController>();
        if (bola != null)
        {
            bola.ResetBola();
        }

        // 2. Resetear el Jugador
        MovimientoJugador player = FindObjectOfType<MovimientoJugador>();
        if (player != null)
        {
            player.ResetPlayer();
        }

        // Asegurarse de que el tiempo esté corriendo normalmente para el juego
        Time.timeScale = 1f;
    }

    /// <summary>
    /// 🚨 FUNCIÓN PARA EMPEZAR EL JUEGO DESDE CERO 🚨
    /// Resetea vidas, puntuación, tiempo y posición de los objetos.
    /// </summary>
    /// 
    public void DestruirPersistentes()
    {
        // 🚨 CLAVE: Destruir el GameObject que contiene la instancia antigua 🚨

        // Destruir Puntuación
        // ASUME: que tienes una variable estática 'Score.instance'
        if (Score.instance != null && Score.instance.gameObject != gameObject)
        {
            Destroy(Score.instance.gameObject);
            
        }

        // Destruir Vidas
        // ASUME: que tienes una variable estática 'Vidas.instance'
        if (Vidas.instance != null && Vidas.instance.gameObject != gameObject)
        {
            Destroy(Vidas.instance.gameObject);
            
        }

        // Destruir Cronómetro
        // ASUME: que tienes una variable estática 'Cronometro.instance'
        if (Cronometro.instance != null && Cronometro.instance.gameObject != gameObject)
        {
            Destroy(Cronometro.instance.gameObject);
            
        }
    }
    public void StartNewGame()
    {
       

        Time.timeScale = 1f;

        // 🚨 CLAVE 🚨: Llama a la función de limpieza ANTES de la recarga
        DestruirPersistentes();

        // La recarga de la escena creará nuevas instancias limpias de todos los Singletons
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
