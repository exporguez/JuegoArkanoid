using UnityEngine;

public class MenuControles : IEstado
{
    public ControlMenus controlMenus;
    public MenuStateMachine menus;
    public void Entrar(MenuStateMachine menus)
    {
        menus.controlMenus.CerrarMenus();
        menus.controlMenus.menuControles.SetActive(true);
    }

    public void Ejecutar(MenuStateMachine menus)
    {

    }

    public void Salir(MenuStateMachine menus)
    {
        menus.controlMenus.menuControles.SetActive(false);
    }
}
