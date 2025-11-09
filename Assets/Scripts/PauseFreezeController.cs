using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseFreezeController : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] float fadeDuration = 0.5f;
    CanvasGroup canvasGroup;
    bool isPaused = false;

    private void Start()
    {
        canvasGroup = canvas.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;     
    }
    void Update()
    {//cuando le damos a la tecla escape se activa la funcion para que se congele
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FreezeCanvas();
        }
        
    }
    //
    void FreezeCanvas()
    {//si presionamos la tecla escape por primera vez lo conegelaremos todo
     //y haremos que salga el menu de pausa
        if (!isPaused)
        {//hacemos que se paren todas las fisicas
            Time.timeScale = 0f;
            StartCoroutine(FadeCanvas(0f, 1f)); // Fade in
            isPaused = true;
        }
        //en caso de estar pausado si le damos al escape otra vez volveremos al juego
        else
        {
            UnFreezeCanvas();
        }
    }

    public void UnFreezeCanvas()
    {//volvemos a dar fisicas
        Time.timeScale = 1f;
        StartCoroutine(FadeCanvas(1f, 0f)); // Fade out
        isPaused = false;
    }
    //hacemos una corrutina para manipular el fadein y faceout
    IEnumerator FadeCanvas(float from, float to)
    {
        float elapsed = 0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        while (elapsed < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / fadeDuration);
            elapsed += Time.unscaledDeltaTime; // Important: use unscaled time when timeScale = 0
            yield return null;
        }

        canvasGroup.alpha = to;

        if (to == 0f)
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
    //un boton para salir del juego
    public void ExitGame()
    {
        Application.Quit();
    }
}
