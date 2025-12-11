using UnityEngine;

public class MenuOpciones : IEstado
{
    public void Entrar(MenuStateMachine menus)
    {       
        menus.controlMenus.menuOpciones.SetActive(true);
    }

    public void Ejecutar(MenuStateMachine menus)
    {

    }

    public void Salir(MenuStateMachine menus)
    {
        menus.controlMenus.menuOpciones.SetActive(false);
    }
}
