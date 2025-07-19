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
        // Tự động tìm UI elements nếu chưa được gán
        if (dialoguePanel == null)
            dialoguePanel = GameObject.Find("DialogueBox");
            
        if (nameText == null)
            nameText = GameObject.Find("Name")?.GetComponent<TextMeshProUGUI>();
            
        if (contentText == null)
            contentText = GameObject.Find("Content")?.GetComponent<TextMeshProUGUI>();
            
        if (characterImage == null)
            characterImage = GameObject.Find("Image")?.GetComponent<Image>();
    }

    private void StartDialogue(string name, string content, Sprite sprite)
    {
        isDialogueActive = true;
        dialoguePanel.SetActive(true);
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
            
        // Hiển thị dialogue panel
        if (dialoguePanel != null)
            dialoguePanel.SetActive(true);
        else
            Debug.LogError("dialoguePanel is null! Please assign DialogueBox GameObject.");
    }

    private void OnEnable()
    {
        EventBus.SetDialogue += StartDialogue;
    }

    private void OnDisable()
    {
        EventBus.SetDialogue -= StartDialogue;
    }
    void Start()
    {
        // Ẩn dialogue panel khi bắt đầu
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }

    void Update()
    {
        // Cho phép đóng dialogue bằng ESC hoặc Space
        if (dialoguePanel != null && dialoguePanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
            {
                CloseDialogue();
            }
        }
    }
    
    public void CloseDialogue()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
        isDialogueActive = false;
    }

    
}
