/*using UnityEngine;

public class Animaciones : MonoBehaviour
{
    public GameObject popUpJugar;

    public LeanTweenType tipoEaseIn = LeanTweenType.easeOutBack;
    public LeanTweenType tipoEaseOut = LeanTweenType.easeInBack;

    private float duracionAnimacion = 0.5f;

    private float tiempoEspera = 2f;

    private int delayedCallID = 0;

    public void MostrarPopUpJugar()
    {
        MostrarPopUp(popUpJugar);
    }

    void MostrarPopUp(GameObject popUp)
    {
        if (popUp == null) return;

        LeanTween.cancel(popUp);

        if (delayedCallID != 0)
        {
            LeanTween.cancel(delayedCallID);
            delayedCallID = 0; // Reseteamos el ID
        }

        popUp.SetActive(true);
        popUp.transform.localScale = Vector3.zero;

        LeanTween.scale(popUp, Vector3.one, duracionAnimacion)
            .setEase(tipoEaseIn)         // Aseguramos que la animación principal está ligada al objeto
            .setOnComplete(() =>
            {
                // 3. Temporizador de cierre: Usamos una sintaxis directa.
                // Quitamos el .setTarget() aquí para evitar posibles conflictos de encadenamiento.
                LTDescr desc = LeanTween.delayedCall(tiempoEspera, () =>
                {
                    OcultarPopUp(popUp);
                });
                delayedCallID = desc.id;
            });
    }

    void OcultarPopUp(GameObject popUp)
    {
        if (popUp == null) return;

        LeanTween.cancel(popUp);

        delayedCallID = 0;

        LeanTween.scale(popUp, Vector3.zero, duracionAnimacion).setEase(tipoEaseOut).setOnComplete(() =>
            {
                popUp.SetActive(false);
                popUp.transform.localScale = Vector3.one;
            });
    }

}*/
