using UnityEngine;

public class MenuPrincipal : IEstado
{
    public void Entrar(MenuStateMachine menus)
    {
        menus.controlMenus.CerrarMenus();
        menus.controlMenus.menuPrincipal.SetActive(true);
    }
    public void Ejecutar(MenuStateMachine menus)
    {

    }
    public void Salir(MenuStateMachine menus)
    {

    }
}
