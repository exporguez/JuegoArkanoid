using UnityEngine;

public class Vidas : MonoBehaviour
{

    public MenuStateMachine menus;

    public static Vidas instance;


    public GameObject[] vidas;

    private int vidasContador;

    public AudioClip derrotaSound;
    public float derrotaVolume = 2f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        vidasContador = vidas.Length;
    }

    public void PerderVidas()
    {
        if (vidasContador <= 0)
        {
            return;
        }

        int indexOcultar = vidasContador - 1;

        if (indexOcultar >= 0 && indexOcultar < vidas.Length)
        {
            vidas[indexOcultar].SetActive(false);
        }

        vidasContador--;

        if (vidasContador == 0)
        {
            Debug.Log("Has perdido");

            ReproducirSonidoDerrota();

            if (Cronometro.instance != null)
            {
                Cronometro.instance.DetenerCronometro();
            }

            BallController.DestruirTodasLasBolas();

            if (Score.instance != null)
            {
                Score.instance.GuardarPuntosTotales();
            }

            if (menus != null && menus.controlMenus != null)
            {
                menus.controlMenus.menuJugar.SetActive(false);
                menus.controlMenus.screenGameOver.SetActive(true);
            }
        }
    }

    public void ReiniciarVidas()
    {
        vidasContador = vidas.Length;

        foreach (GameObject vidaIcono in vidas)
        {
            if (vidaIcono != null)
            {
                vidaIcono.SetActive(true);
            }
        }
    }

    public void ReproducirSonidoDerrota()
    {
        AudioSource.PlayClipAtPoint(derrotaSound, transform.position, derrotaVolume);
    }

    public void ResetVidas()
    {
        vidasContador = vidas.Length;
    }
}
