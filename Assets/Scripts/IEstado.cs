//Todos los estados tendrán lo que está aquí
public interface IEstado
{
    void Entrar(MenuStateMachine menus);
    void Ejecutar(MenuStateMachine menus);
    void Salir(MenuStateMachine menus);

}

