using UnityEngine;
using UnityEngine.UI;

public class ControlMenus : MonoBehaviour
{
    public static ControlMenus Instance { get; private set; }
    //public MenuStateMachine menus;
    public ControlMenus controlMenus;

    //Menus
    public GameObject menuPrincipal;
    public GameObject menuJugar;
    public GameObject menuSalir;
    public GameObject menuControles;
    public GameObject menuOpciones;
    public GameObject menuPause;

    public GameObject escenario;
    public GameObject screenGameOver;
    public GameObject screenVictoria;

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
        VolverMenuPrincipal();
    }

    public void CerrarMenus() // Función para cerrar todos los menús
    {
        menuPrincipal.SetActive(false);
        menuJugar.SetActive(false);
        menuSalir.SetActive(false);
        menuControles.SetActive(false);
        menuOpciones.SetActive(false);
        menuPause.SetActive(false);
        escenario.SetActive(false);
        screenGameOver.SetActive(false);
        screenVictoria.SetActive(false);
        popUpJugar.SetActive(false);
    }

    public void VolverMenuPrincipal()
    {
        CerrarMenus();
        menuPrincipal.SetActive(true);
    }

    public void AbrirMenuJugar()
    {
        CerrarMenus();
        menuJugar.SetActive(true);
        escenario.SetActive(true);
        popUpJugar.SetActive(true);
    }

    public void AbrirMenuControles()
    {
        CerrarMenus();
        menuControles.SetActive(true);
    }

    public void AbrirMenuOpciones()
    {
        CerrarMenus();
        menuOpciones.SetActive(true);
    }

    public void AbrirMenuPause()
    {       
        menuPause.SetActive(true);
    }

    public void AbrirMenuSalir()
    {       
        menuSalir.SetActive(true);
    }
}
