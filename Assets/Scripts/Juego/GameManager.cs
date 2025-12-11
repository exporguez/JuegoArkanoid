using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
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

    
    public void ResetPlayerAndBall()
    {
               
        BallController bola = FindObjectOfType<BallController>();
        if (bola != null)
        {
            bola.ResetBola();
        }

        
        MovimientoJugador player = FindObjectOfType<MovimientoJugador>();
        if (player != null)
        {
            player.ResetPlayer();
        }
        
        Time.timeScale = 1f;
    }

    
    public void DestruirPersistentes()
    {
        
        if (Score.instance != null && Score.instance.gameObject != gameObject)
        {
            Destroy(Score.instance.gameObject);
            
        }

        if (Vidas.instance != null && Vidas.instance.gameObject != gameObject)
        {
            Destroy(Vidas.instance.gameObject);
            
        }
       
        if (Cronometro.instance != null && Cronometro.instance.gameObject != gameObject)
        {
            Destroy(Cronometro.instance.gameObject);
            
        }
    }
    public void StartNewGame()
    {
       
        Time.timeScale = 1f;
        
        DestruirPersistentes();
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}
