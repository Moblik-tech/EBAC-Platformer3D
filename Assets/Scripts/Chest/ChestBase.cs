using UnityEngine;
using DG.Tweening;

public class ChestBase : MonoBehaviour
{
    public KeyCode openKeycode = KeyCode.E;
    public Animator animator;
    public string triggerOpenName = "Open";
    private bool _chestOpened = false;

    [Header("Notification")]
    public GameObject notificationIcon;
    public float tweenDuration = 0.2f;
    public Ease tweenEase = Ease.OutBack;
    [Space(15)]
    public ChestItemBase chestItem;
    public float coinDelay = 0.3f;

    private void Start()
    {
        UpdateNotificationStatus(false);
    }

    private void OpenChest()
    {
        if (_chestOpened == true) return;

        animator.SetTrigger(triggerOpenName);
        UpdateNotificationStatus(false);
        _chestOpened = true;

        Invoke(nameof(ShowItem), coinDelay);
    }

    private void ShowItem()
    {
        chestItem.ShowItem();
        Invoke(nameof(CollectItem), coinDelay);
    }

    private void CollectItem()
    {
        chestItem.Collect();
    }

    public void OnTriggerEnter(Collider other)
    {
        PlayerController p = other.transform.GetComponent<PlayerController>();

        if (p != null)
        {
            UpdateNotificationStatus(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        PlayerController p = other.transform.GetComponent<PlayerController>();

        if (p != null)
        {
            UpdateNotificationStatus(false);
        }
    }

    private void UpdateNotificationStatus(bool showNotification)
    {
        if (_chestOpened == true) return;

        notificationIcon.SetActive(showNotification);
        notificationIcon.transform.DOScale(0, tweenDuration).SetEase(tweenEase).From();
    }

    private void Update()
    {
        if (Input.GetKeyDown(openKeycode) && notificationIcon.activeSelf)
        {
            OpenChest();
        }
    }
}