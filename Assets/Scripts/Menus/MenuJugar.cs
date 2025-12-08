using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MenuJugar : IEstado
{

    //public Animaciones animaciones;

    public void Entrar(MenuStateMachine menus)
    {
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
