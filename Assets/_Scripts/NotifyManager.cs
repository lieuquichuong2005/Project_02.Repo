using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class NotificationManager : MonoBehaviour
{
    public GameObject notificationPrefab; 
    public Transform notificationPanel; 

    private Queue<GameObject> notifications = new Queue<GameObject>();

    public void ShowNotification(Sprite itemSprite, string itemName)
    {
        GameObject notification = Instantiate(notificationPrefab, notificationPanel);
        notification.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = itemSprite;
        notification.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = "+ " + itemName;
        
        notifications.Enqueue(notification);
        StartCoroutine(DisplayNotifications());
    }

    private IEnumerator DisplayNotifications()
    {
        while (notifications.Count > 0)
        {
            GameObject currentNotification = notifications.Dequeue();
            currentNotification.SetActive(true);

            yield return new WaitForSeconds(3f); // Hiển thị trong 3 giây

            Destroy(currentNotification); // Xóa thông báo sau khi hiển thị
        }
    }
}