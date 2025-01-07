using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    [Header("UI References")]
    public Image playerAvatar;               // Khung avatar của Player
    public Image npcAvatar;                  // Khung avatar của NPC
    public GameObject dialogueSystem;        // Panel nền chính
    public Transform dialogueContents;       // Container chứa các câu thoại
    public GameObject dialoguePrefab;        // Prefab hiển thị câu thoại
    public ScrollRect scrollRect;            // Để cuộn tự động

    private bool isDialogueActive = false;   // Trạng thái đối thoại
    private bool isTyping = false;           // Kiểm tra xem có đang gõ chữ không

    private void Start()
    {
        // Đặt avatar cho Player và NPC khi bắt đầu trò chơi
        SetAvatars(PlayerMovement.instance.playerAvatar, PlayerMovement.instance.playerAvatar);

        // Bắt đầu hội thoại mẫu
        //StartCoroutine(TestDialogue());
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
        // Bật panel nền khi bắt đầu nói chuyện
        if (!isDialogueActive)
        {
            dialogueSystem.SetActive(true);
            isDialogueActive = true;
        }
        StartCoroutine(TestDialogue());
    }

    public void EndDialogue()
    {
        // Tắt panel nền khi kết thúc nói chuyện
        dialogueSystem.SetActive(false);
        isDialogueActive = false;

        // Xóa các câu thoại trong container
        foreach (Transform child in dialogueContents)
        {
            Destroy(child.gameObject);
        }
    }

    public IEnumerator DisplayDialogueCoroutine(string characterName, string dialogue)
    {
        // Đợi nếu đang gõ chữ
        while (isTyping)
        {
            yield return null;
        }

        isTyping = true;

        // Tạo prefab hiển thị câu thoại
        GameObject dialogueInstance = Instantiate(dialoguePrefab, dialogueContents);

        // Cấu hình hiển thị của prefab
        TextMeshProUGUI dialogueText = dialogueInstance.GetComponentInChildren<TextMeshProUGUI>();
        dialogueText.text = ""; // Khởi tạo văn bản trống

        RectTransform rectTransform = dialogueInstance.GetComponent<RectTransform>();

        // Căn chỉnh và vị trí câu thoại tùy thuộc vào nhân vật nói
        if (characterName == "Player")
        {
            dialogueText.alignment = TextAlignmentOptions.Left; // Căn trái
            rectTransform.localPosition = new Vector3(200, rectTransform.localPosition.y, 0); // Đẩy sang phải
        }
        else if (characterName == "NPC")
        {
            dialogueText.alignment = TextAlignmentOptions.Right;  // Căn phải
            rectTransform.localPosition = new Vector3(-200, rectTransform.localPosition.y, 0); // Đẩy sang trái
        }

        // Gọi coroutine để thực hiện hiệu ứng gõ chữ
        yield return StartCoroutine(TypewriterEffect(dialogueText, dialogue));

        isTyping = false;  // Đánh dấu kết thúc việc gõ chữ
        ScrollToBottom();
    }

    private void ScrollToBottom()
    {
        Canvas.ForceUpdateCanvases(); // Bắt Unity cập nhật layout ngay lập tức
        scrollRect.verticalNormalizedPosition = 0f; // Đặt cuộn ở đáy (0f là dưới cùng)
    }

    private IEnumerator TypewriterEffect(TextMeshProUGUI dialogueText, string dialogue)
    {
        for (int i = 0; i < dialogue.Length; i++)
        {
            dialogueText.text += dialogue[i];  // Thêm từng ký tự vào text
            yield return new WaitForSeconds(0.05f);  // Thời gian giữa các ký tự (thay đổi nếu cần)
        }
    }

    // Hội thoại mẫu để kiểm tra
    private IEnumerator TestDialogue()
    {
        yield return new WaitForSeconds(1);

        yield return StartCoroutine(DisplayDialogueCoroutine("NPC", "Chào mừng bạn đến ngôi làng Thanh Bình!"));
        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(DisplayDialogueCoroutine("NPC", "Ngôi làng này nổi tiếng với sự yên bình và những cảnh quan tuyệt đẹp."));
        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(DisplayDialogueCoroutine("Player", "Ngôi làng này có gì đặc biệt?"));
        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(DisplayDialogueCoroutine("NPC", "Chúng tôi có một lễ hội mùa xuân nổi tiếng diễn ra hàng năm."));
        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(DisplayDialogueCoroutine("Player", "Lễ hội đó diễn ra thế nào?"));
        yield return new WaitForSeconds(2f);

        yield return StartCoroutine(DisplayDialogueCoroutine("NPC", "Bạn sẽ thấy múa lân, hội chợ, và các hoạt động văn hóa truyền thống."));
        yield return new WaitForSeconds(2.5f);

        yield return StartCoroutine(DisplayDialogueCoroutine("NPC", "Ngoài ra, còn có những món ăn ngon mà bạn không thể bỏ lỡ!"));
        yield return new WaitForSeconds(2.5f);

        yield return StartCoroutine(DisplayDialogueCoroutine("Player", "Nghe thú vị quá. Người dân ở đây thế nào?"));
        yield return new WaitForSeconds(2f);

        yield return StartCoroutine(DisplayDialogueCoroutine("NPC", "Mọi người rất thân thiện và sẵn lòng giúp đỡ nhau."));
        yield return new WaitForSeconds(2.5f);

        yield return StartCoroutine(DisplayDialogueCoroutine("NPC", "Chúng tôi còn làm nghề thủ công truyền thống, như gốm sứ và dệt vải."));
        yield return new WaitForSeconds(2.5f);

        yield return StartCoroutine(DisplayDialogueCoroutine("Player", "Bạn có thể chỉ đường đến nơi tôi có thể xem những sản phẩm đó không?"));
        yield return new WaitForSeconds(2f);

        yield return StartCoroutine(DisplayDialogueCoroutine("NPC", "Tất nhiên! Đi thẳng đến chợ làng, bạn sẽ thấy rất nhiều sản phẩm thủ công."));
        yield return new WaitForSeconds(2.5f);

        yield return StartCoroutine(DisplayDialogueCoroutine("Player", "Cảm ơn bạn. Tôi rất muốn khám phá thêm."));
        yield return new WaitForSeconds(2f);

        yield return StartCoroutine(DisplayDialogueCoroutine("NPC", "Không có gì. Chào mừng bạn đến thăm làng!"));
        yield return new WaitForSeconds(2.5f);

        EndDialogue();
    }
}
