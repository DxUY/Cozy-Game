using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI contentText;
    public Image characterImage;
    public GameObject dialoguePanel;
    public bool isDialogueActive = false;

    private void Awake()
    {
        if (nameText == null)
            nameText = GameObject.Find("Name")?.GetComponent<TextMeshProUGUI>();
            
        if (contentText == null)
            contentText = GameObject.Find("Content")?.GetComponent<TextMeshProUGUI>();
            
        if (characterImage == null)
            characterImage = GameObject.Find("Image")?.GetComponent<Image>();
            
        if (dialoguePanel == null)
            dialoguePanel = GameObject.Find("Dialogue");
            
        // Ẩn dialogue panel khi khởi tạo
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }

    private void StartDialogue(string name, string content, Sprite sprite)
    {
        isDialogueActive = true;
        
        // Hiển thị dialogue panel
        if (dialoguePanel != null)
            dialoguePanel.SetActive(true);
        else
            Debug.LogError("dialoguePanel is null! Please assign Dialogue GameObject.");
        
        // Kiểm tra null trước khi assign
        if (nameText != null)
            nameText.text = name;
        else
            Debug.LogError("nameText is null! Please assign Name TextMeshPro component.");
            
        if (contentText != null)
            contentText.text = content;
        else
            Debug.LogError("contentText is null! Please assign Content TextMeshPro component.");
            
        if (characterImage != null)
            characterImage.sprite = sprite;
        else
            Debug.LogError("characterImage is null! Please assign Image component.");
    }

    private void OnEnable()
    {
        EventBus.SetDialogue += StartDialogue;
    }

    private void OnDisable()
    {
        EventBus.SetDialogue -= StartDialogue;
    }

    // Unity lifecycle method - called every frame
    void Update()
    {
        // Debug để kiểm tra ESC input
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log($"DialogueManager: ESC pressed, isDialogueActive = {isDialogueActive}");
        }
        
        // Cho phép đóng dialogue bằng ESC hoặc Space
        if (isDialogueActive == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("DialogueManager: Closing dialogue");
                CloseDialogue();
            }
        }
    }
    
    public void CloseDialogue()
    {
        isDialogueActive = false;
        
        // Ẩn dialogue panel
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }

    
}
