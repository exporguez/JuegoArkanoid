using UnityEngine;

public class MenuPause : IEstado
{
    public void Entrar(MenuStateMachine menus)
    {
        menus.controlMenus.CerrarMenus();
        menus.controlMenus.menuPause.SetActive(true);
    }
    public void Ejecutar(MenuStateMachine menus)
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //|| Input.GetKeyDown(KeyCode.))
        {
            menus.IrMenuPause();
        }
    }
    public void Salir(MenuStateMachine menus)
    {
        menus.controlMenus.menuPause.SetActive(false);
    }
}
