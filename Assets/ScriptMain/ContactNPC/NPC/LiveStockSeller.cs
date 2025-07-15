using UnityEngine;
using UnityEngine.UI;

public class LiveStockSeller : MonoBehaviour
{
    public GameObject confirmPanel;
    public Button yesButton;
    public Button noButton;
    public GameObject buyCanvas;

    private bool playerInRange = false;
    private AnimalType selectedType = AnimalType.None;

    [Header("UI Button Access")]
    public Button WhiteSheepButton;
    public Button BlackSheepButton;
    public Button CreamSheepButton;
    public Button WhiteGoatButton;
    public Button BlackGoatButton;

    [Header("Spawn Point And Moving Random Point")]
    public Transform spawnPoint;
    public Transform spawnPoint2;

    private void Start()
    {
        buyCanvas.gameObject.SetActive(false);
        confirmPanel.SetActive(false);

        yesButton.onClick.AddListener(OnConfirmPurchase);
        noButton.onClick.AddListener(OnCancelPurchase);

        WhiteGoatButton.onClick.AddListener(() => SelectAnimal(AnimalType.WhiteGoat));
        BlackGoatButton.onClick.AddListener(() => SelectAnimal(AnimalType.BlackGoat));
        WhiteSheepButton.onClick.AddListener(() => SelectAnimal(AnimalType.WhiteSheep));
        CreamSheepButton.onClick.AddListener(() => SelectAnimal(AnimalType.CreamSheep));
        BlackSheepButton.onClick.AddListener(() => SelectAnimal(AnimalType.BlackSheep));
    }

    void SelectAnimal(AnimalType type)
    {
        selectedType = type;
        confirmPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    void OnConfirmPurchase()
    {
        if (selectedType != AnimalType.None)
        {
            AnimalFactory.CreateAnimal(selectedType, spawnPoint.position);
        }
        confirmPanel.SetActive(false);
        buyCanvas.gameObject.SetActive(false);
        selectedType = AnimalType.None;
    }

    void OnCancelPurchase()
    {
        confirmPanel.SetActive(false);
    }
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            buyCanvas.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    void OnChoose()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            buyCanvas.gameObject.SetActive(false);
            confirmPanel.SetActive(false);
            selectedType = AnimalType.None;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
