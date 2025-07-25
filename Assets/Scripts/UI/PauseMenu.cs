using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [Header("Pause Menu Panel")]
    public GameObject pauseMenuPanel;
    
    private bool isPaused = false;
    
    private void Start()
    {
        Debug.Log("PauseMenu Start() called");
        
        // Ẩn pause menu khi game bắt đầu
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false);
            isPaused = false;
            Debug.Log("Pause menu panel hidden on start");
        }
        else
        {
            Debug.LogError("pauseMenuPanel is null in Start()!");
        }
        
        // Đảm bảo thời gian game chạy bình thường
        Time.timeScale = 1f;
    }
    
    private void Update()
    {
        // Debug để xem Update có chạy không
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("PauseMenu: P key pressed!");
            if (isPaused == false)
            {
                Debug.Log("Game is not paused, calling PauseGame()");
                PauseGame();
            }
            else
            {
                Debug.Log("Game is paused, calling ContinueGame()");
                ContinueGame();
            }
        }
        
        // Test với key khác để đảm bảo Update chạy
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("PauseMenu Update() is working - O key pressed!");
        }
    }
    
    // Function để gán vào Continue Button OnClick
    public void ContinueGame()
    {
        Debug.Log("ContinueGame() called");
        isPaused = false;
        Time.timeScale = 1f; // Khôi phục thời gian game
        
        // Ẩn pause menu
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false);
            Debug.Log("Pause menu hidden");
        }
        else
        {
            Debug.LogError("pauseMenuPanel is null! Please assign it in Inspector");
        }
            
        Debug.Log("Game Continued");
    }
    
    // Function để gán vào Quit Button OnClick  
    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    
    // Function để pause game (có thể gọi từ code khác)
    public void PauseGame()
    {
        Debug.Log("PauseGame() called");
        isPaused = true;
        Time.timeScale = 0f; // Dừng thời gian game
        
        // Hiển thị pause menu
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(true);
            Debug.Log("Pause menu shown");
        }
        else
        {
            Debug.LogError("pauseMenuPanel is null! Please assign it in Inspector");
        }
        
        Debug.Log("Game Paused");
    }
}
