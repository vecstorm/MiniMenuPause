using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropdownResolutionManager : MonoBehaviour
{
    [SerializeField] TMP_Dropdown resolutionDropdown;
    [SerializeField] Toggle fullscreenToggle;

    //Para guardar todas las resoluciones del dispositivo
    private Resolution[] allResolutions;
    private List<Resolution> filteredResolutions = new List<Resolution>();
    //usamos claves para poder recuperar los valores de playerPrefs
    private const string ResolutionKey = "SelectedResolution";
    private const string FullscreenKey = "Fullscreen";

    void Start()
    {
        // Obtener resoluciones únicas sin repetir tasa de refresco
        allResolutions = Screen.resolutions;
        //evitamos duplicados por tamaño
        HashSet<string> seen = new HashSet<string>();
        List<string> options = new List<string>();

        //recorremos todas las resoluciones y si hay una resolucion que no ha visto antes,
        //la añade. Evitamos repetit resoluciones repetidas con distintas tasas de refresco
        foreach (Resolution res in allResolutions)
        {
            string key = res.width + "x" + res.height;
            if (!seen.Contains(key))
            {
                seen.Add(key);
                filteredResolutions.Add(res);
                options.Add(key);
            }
        }
        //limpiamos y guardamos las resoluciones en el dropdown
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);

        // Cargar resolución guardada y actualiza el dropdown
        int savedIndex = PlayerPrefs.GetInt(ResolutionKey, filteredResolutions.Count - 1);
        resolutionDropdown.value = savedIndex;
        resolutionDropdown.RefreshShownValue();

        // Cargamos la pantalla completa guardada y usamos true por defecto
        bool isFullscreen = PlayerPrefs.GetInt(FullscreenKey, 1) == 1;
        fullscreenToggle.isOn = isFullscreen;
        //aplicamos la resolucion
        ApplyResolution(savedIndex, isFullscreen);

        // añadimos listeners para hacer los cambios cuando el usuario cambie las resoluciones
        //o la pantalla completa
        resolutionDropdown.onValueChanged.AddListener(OnResolutionChanged);
        fullscreenToggle.onValueChanged.AddListener(OnFullscreenToggled);
    }
    //aplicamos la nueva resolucion y la gurdamos en PlayerPrefs
    void OnResolutionChanged(int index)
    {
        ApplyResolution(index, fullscreenToggle.isOn);
        PlayerPrefs.SetInt(ResolutionKey, index);
    }
    //aplicamos la pantalla completa y la guardamos en playerPrefs
    void OnFullscreenToggled(bool isFullscreen)
    {
        ApplyResolution(resolutionDropdown.value, isFullscreen);
        PlayerPrefs.SetInt(FullscreenKey, isFullscreen ? 1 : 0);
    }
    //cambiamos la resolucion del juego e indicamos si se mantiene en pantalla completa
    void ApplyResolution(int index, bool isFullscreen)
    {
        Resolution res = filteredResolutions[index];
        Screen.SetResolution(res.width, res.height, isFullscreen);
    }
}
