using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgainScript : MonoBehaviour
{
    private void Update()
    {
        CheckForTapInput();

        CheckForClickInput();
    }

    private void CheckForTapInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);

            if (Physics.Raycast(ray, out RaycastHit hit, 100.0f))
            {
                if (hit.transform != null )
                {
                    Debug.Log("Reload Main");
                    SceneManager.LoadScene("Main", LoadSceneMode.Single);
                }
            }
        }
    }

    private void CheckForClickInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Clicked!");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100.0f))
            {
                if (hit.transform != null)
                {
                    Debug.Log("Reload Main");
                    SceneManager.LoadScene("Main", LoadSceneMode.Single);
                }
            }
        }
    }
}