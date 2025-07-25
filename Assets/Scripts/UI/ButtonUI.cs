using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonUI : MonoBehaviour
{
    [SerializeField] private string ContinueButton;
    [SerializeField] private string QuitButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Continue()
    {
        SceneManager.LoadScene(ContinueButton);
    }

    public void Quit()
    {
        SceneManager.LoadScene(QuitButton);
    }
}
