using UnityEngine;

public class Vidas : MonoBehaviour
{

    public MenuStateMachine menus;

    public static Vidas instance;

    
    public GameObject[] vidas;
    
    private int vidasContador;

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
        if(vidasContador > 0)
        {
            int indexOcultar = vidasContador - 1;

            if(indexOcultar >= 0 && indexOcultar < vidas.Length)
            {
                vidas[vidasContador].SetActive(false);
            }
        }

        vidasContador--;
        

        if(vidasContador == 0)
        {
            Debug.Log("Has perdido");

            if(Score.instance != null)
            {
                Score.instance.GuardarPuntosTotales();
            }
            
            //if(menus != null && menus.controlMenus != null)
            //menus.controlMenus.screenGameOver.SetActive(true);
        }
    }

}
