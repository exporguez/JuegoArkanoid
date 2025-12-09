using UnityEngine;


public class MenuJugar : IEstado
{

    //public Animaciones animaciones;

    public void Entrar(MenuStateMachine menus)
    {
        if (Vidas.instance != null)
        {
            Vidas.instance.ReiniciarVidas();
        }

        if (Score.instance != null)
        {
            Score.instance.ReiniciarPuntuacion();
            Score.instance.ActualizarTexto();

        }
        

        if (BallController.instance != null)
        {
            BallController.instance.ReiniciarPelota();
        }

        Cronometro.instance.ReiniciarCronometro();
        menus.controlMenus.CerrarMenus();
        menus.controlMenus.menuJugar.SetActive(true);
        menus.controlMenus.escenario.SetActive(true);


        
    }

    public void Ejecutar(MenuStateMachine menus)
    {       

    }

    public void Salir(MenuStateMachine menus)
    {
        menus.controlMenus.menuJugar.SetActive(false);
        menus.controlMenus.escenario.SetActive(false);
    }

}
