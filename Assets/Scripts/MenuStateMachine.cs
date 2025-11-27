using UnityEngine;

public class MenuStateMachine : MonoBehaviour
{
    public ControlMenus controlMenus;

    private IEstado estadoAtualMenu;

    public void Start()
    {
        VolverMenuPrincipal();
    }

    public void Update()
    {
        if (estadoAtualMenu != null)// Si hay un estado actual, ejecuta su lógica
        {
            estadoAtualMenu.Ejecutar(this);// Ejecuta la lógica del estado actual
        }
    }

    public void CambiarEstado(IEstado nuevoEstado)
    {
        // Cambia el estado actual del menú
        if (estadoAtualMenu != null)
        {
            estadoAtualMenu.Salir(this);
        }

        // Pasamos al nuevo estado
        estadoAtualMenu = nuevoEstado;
        // Entramos en el nuevo estado
        estadoAtualMenu.Entrar(this);
    }

    public void IrMenuJugar()
    {
        CambiarEstado(new MenuJugar());
    }

    public void IrMenuSalir()
    {
        CambiarEstado(new MenuSalir());
    }

    public void VolverMenuPrincipal()
    {
        CambiarEstado(new MenuPrincipal());
    }

    public void ReiniciarPartida()
    {

    }

    public void ModoPause()
    {

    }
}
