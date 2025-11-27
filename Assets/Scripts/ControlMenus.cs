using UnityEngine;
using UnityEngine.UI;

public class ControlMenus : MonoBehaviour
{
    public static ControlMenus Instance { get; private set; }
    public MenuStateMachine menus;
    public ControlMenus controlMenus;

    //Menus
    public GameObject menuPrincipal;
    public GameObject menuJugar;
    public GameObject menuSalir;
    public GameObject menuControles;

    //Botones Menu Principal
    public Button botonJugar;
    public Button botonSalir;

    //Botones Menu Jugar
    public Button botonPause;

    //Botones Menu Controles
    /// <summary>
    /// - boton volver al menu principal - 
    /// </summary>
    
    //Botones Menu Pause
    public Button botonReanudar;
    /// <summary>
    /// - boton controles - 
    /// - boton volver al menu principal - 
    /// </summary>   

    //Botones Game Over
    public Button botonVolverAlMenuPrincipal;
    public Button botonReiniciarPartida;

    //PopUps
    public GameObject popUpJugar;

    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public void Start()
    {
        //botones Menu Principal
        botonJugar.onClick.AddListener(() => menus.IrMenuJugar());
        botonSalir.onClick.AddListener(() => menus.IrMenuSalir());

        //botones Game Over
        botonVolverAlMenuPrincipal.onClick.AddListener(() => menus.VolverMenuPrincipal());
        botonReiniciarPartida.onClick.AddListener(() => menus.ReiniciarPartida());

        //botones Menu Jugar
        botonPause.onClick.AddListener(() => menus.ModoPause());

        CerrarMenus();
        menuPrincipal.SetActive(true);
    }
    public void CerrarMenus() // Función para cerrar todos los menús
    {
        menuPrincipal.SetActive(false);
        menuJugar.SetActive(false);
        menuSalir.SetActive(false);
        menuControles.SetActive(false);
    }
}
