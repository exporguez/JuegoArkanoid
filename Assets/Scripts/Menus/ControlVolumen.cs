using UnityEngine;
using UnityEngine.UI;

public class ControlVolumen : MonoBehaviour
{
    public Slider volumenSlider;
    
    private const string VolumenKey = "MasterVolume";

    void Start()
    {       
        CargarVolumen();
        
        if (volumenSlider != null)
        {          
            volumenSlider.onValueChanged.AddListener(CambiarVolumen);
        }
    }
    
    public void CambiarVolumen(float nuevoVolumen)
    {       
        AudioListener.volume = nuevoVolumen;
        
        PlayerPrefs.SetFloat(VolumenKey, nuevoVolumen);
    }

    void CargarVolumen()
    {        
        float volumenGuardado = PlayerPrefs.GetFloat(VolumenKey, 1f);
        
        AudioListener.volume = volumenGuardado;
        
        if (volumenSlider != null)
        {
            volumenSlider.value = volumenGuardado;
        }
    }

    
}


