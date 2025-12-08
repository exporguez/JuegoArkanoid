using UnityEngine;

public class Vidas : MonoBehaviour
{

    public MenuStateMachine menus;

    public static Vidas instance;

    [SerializeField]
    public GameObject[] vidas;

    
    private int vidasContador;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);

        vidasContador = vidas.Length;
    }

    public void PerderVidas()
    {
        vidasContador--;

        vidas[vidasContador].SetActive(false);

        if(vidasContador == 0)
        {
            Debug.Log("Has perdido");
            Score.instance.GuardarPuntosTotales();
            //menus.controlMenus.screenGameOver.SetActive(true);
        }
    }

}
