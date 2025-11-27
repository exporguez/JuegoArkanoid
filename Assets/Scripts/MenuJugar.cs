using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MenuJugar : IEstado
{
    public MenuStateMachine menus;

    public float movimientoEjeX;
    public float velocidadMovimiento = 1.5f;

    public GameObject pala;//objeto que se mueve

    public void Entrar(MenuStateMachine menus)
    {
        menus.controlMenus.CerrarMenus();
        menus.controlMenus.menuJugar.SetActive(true);

    }
    
    public void Ejecutar(MenuStateMachine menus)
    {
        movimientoEjeX = Input.GetAxis("Horizontal") * Time.deltaTime * velocidadMovimiento;
        pala.transform.Translate(movimientoEjeX, 0f, 0f);
    }
    
    public void Salir(MenuStateMachine menus)
    {
        menus.controlMenus.menuJugar.SetActive(false);
    }
}
