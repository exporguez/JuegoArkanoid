using UnityEngine;

public class MenuSalir : IEstado
{
    public void Entrar(MenuStateMachine menus)
    {

        if (ControlMenus.Instance != null)
        {

            ControlMenus.Instance.menuSalir.SetActive(true);


            ControlMenus.Instance.AnimarEntradaPopUps(ControlMenus.Instance.popUpSalir);
        }
    }
    public void Ejecutar(MenuStateMachine menus)
    {

    }
    public void Salir(MenuStateMachine menus)
    {
        menus.controlMenus.menuSalir.SetActive(false);
    }
}
