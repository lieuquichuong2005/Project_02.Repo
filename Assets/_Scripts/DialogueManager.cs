using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public ShopManager shopManager;

    [Header("UI References")]
    public Image playerAvatar;               
    public Image npcAvatar;                  
    public GameObject dialogueSystem;        
    public Transform dialogueContents;       
    public GameObject dialoguePrefab;        
    public ScrollRect scrollRect;

    public GameObject buttonDialogue;
    public Button shopButton;
    public Button missonButton;
    public Button talkButton;
    public Button closeDialogueButton;

    private bool isDialogueActive = false;   
    private bool isTyping = false;         

    private void Start()
    {
        // Đặt avatar cho Player và NPC khi bắt đầu trò chơi
        //SetAvatars(PlayerMovement.instance.playerAvatar, PlayerMovement.instance.playerAvatar);

        buttonDialogue.SetActive(false);
        // Bắt đầu hội thoại mẫu
        //StartCoroutine(TestDialogue());

        shopButton.onClick.AddListener(OnShopButtonClick);
        missonButton.onClick.AddListener(OnMissonButtonClick);
        talkButton.onClick.AddListener(OnTalkButtonClick);
        closeDialogueButton.onClick.AddListener(OnCloseDialogueButtonClick);
    }

    public void SetAvatars(Sprite playerSprite, Sprite npcSprite)
    {
        playerAvatar.sprite = playerSprite;
        npcAvatar.sprite = npcSprite;

        playerAvatar.gameObject.SetActive(true);
        npcAvatar.gameObject.SetActive(true);
    }

    public void StartDialogue()
    {
        if (!isDialogueActive)
        {
            dialogueSystem.SetActive(true);
            isDialogueActive = true;
        }
        StartCoroutine(TestDialogue());
    }

    public void EndDialogue()
    {
        dialogueSystem.SetActive(false);
        isDialogueActive = false;

        foreach (Transform child in dialogueContents)
        {
            Destroy(child.gameObject);
        }
        buttonDialogue.SetActive(false);
    }

    public IEnumerator DisplayDialogueCoroutine(string characterName, string dialogue)
    {
        while (isTyping)
        {
            yield return null;
        }

        isTyping = true;

        GameObject dialogueInstance = Instantiate(dialoguePrefab, dialogueContents);
        ScrollToBottom();

        TextMeshProUGUI dialogueText = dialogueInstance.GetComponentInChildren<TextMeshProUGUI>();
        dialogueText.text = "";

        RectTransform rectTransform = dialogueInstance.GetComponent<RectTransform>();

        if (characterName == "Player")
        {
            dialogueText.alignment = TextAlignmentOptions.Left;
            rectTransform.localPosition = new Vector3(200, rectTransform.localPosition.y, 0);
        }
        else if (characterName == "NPC")
        {
            dialogueText.alignment = TextAlignmentOptions.Right;
            rectTransform.localPosition = new Vector3(-200, rectTransform.localPosition.y, 0);
        }

        yield return StartCoroutine(TypewriterEffect(dialogueText, dialogue));

        isTyping = false; 
    }

    private void ScrollToBottom()
    {
        Canvas.ForceUpdateCanvases(); 
        scrollRect.verticalNormalizedPosition = 0f; 
    }

    private IEnumerator TypewriterEffect(TextMeshProUGUI dialogueText, string dialogue)
    {
        for (int i = 0; i < dialogue.Length; i++)
        {
            dialogueText.text += dialogue[i];
            yield return new WaitForSeconds(0.03f);  
        }
    }

    private IEnumerator TestDialogue()
    {
        yield return new WaitForSeconds(0.5f);

        yield return StartCoroutine(DisplayDialogueCoroutine("NPC", "Chào mừng bạn đến với Làng Tân Thủ, đây sẽ là nơi bạn bắt đầu."));
        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(DisplayDialogueCoroutine("NPC", "Tôi là thợ rèn của ngôi làng này, bạn có thể mua bán trao đổi các loại giáp và nhận nhiệm vụ với tôi."));
        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(DisplayDialogueCoroutine("NPC", "Phía trên có một thầy thuốc, hãy thử làm quen với mọi người trong ngôi làng này."));
        yield return new WaitForSeconds(2f);

        yield return StartCoroutine(DisplayDialogueCoroutine("Player", "Cảm ơn bạn."));
        yield return new WaitForSeconds(2f);

        yield return StartCoroutine(DisplayDialogueCoroutine("NPC", "Không có gì. Chào mừng bạn đến thăm làng!"));
        yield return new WaitForSeconds(2.5f);

        buttonDialogue.SetActive(true);
    }
    void OnShopButtonClick()
    {
        if (PlayerMovement.instance.isNearToBlacksmithNPC)
            PlayerMovement.instance.OpenShop("Armor");
        else if (PlayerMovement.instance.isNearToHerbalistNPC)
            PlayerMovement.instance.OpenShop("Consume");
        EndDialogue();
    }
    void OnMissonButtonClick()
    {
        Debug.Log("Misson Button Clicked");
    }
    void OnTalkButtonClick()
    {
        Debug.Log("Talk Button Click");
    }
    void OnCloseDialogueButtonClick()
    {
        EndDialogue();

    }
}
