using UnityEngine;
using UnityEngine.UI;

public class TeamSelectionManager : MonoBehaviour
{
    public GameObject teamSelectionPanel; // Takým seçim paneli
    public Button teamSelectionButton;    // Takým seçimi açan buton
    public Button redTeamButton;          // Kýrmýzý takým butonu
    public Button blueTeamButton;         // Mavi takým butonu

    public MenuMenager menuMenager;      // MenuMenager nesnesi referansý

    void Start()
    {
        // Paneli baþlangýçta gizle
        teamSelectionPanel.SetActive(false);

        // Eðer menuMenager atanmamýþsa, FindObjectOfType ile bulmaya çalýþ
        if (menuMenager == null)
        {
            menuMenager = FindObjectOfType<MenuMenager>();
        }

        // Butonlara týklanýldýðýnda çalýþacak fonksiyonlarý atama
        teamSelectionButton.onClick.AddListener(ShowTeamSelectionPanel);
        redTeamButton.onClick.AddListener(SelectRedTeam);
        blueTeamButton.onClick.AddListener(SelectBlueTeam);
    }

    void Update()
    {
        // ESC tuþuna basýldýðýnda paneli kapat
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (teamSelectionPanel.activeSelf) // Panel açýk mý kontrol et
            {
                teamSelectionPanel.SetActive(false); // Paneli kapat
            }
        }
    }
    void ShowTeamSelectionPanel()
    {
        teamSelectionPanel.SetActive(true);
    }

    void SelectRedTeam()
    {
        if (menuMenager != null)
        {
            // Kýrmýzý takýmý seç ve MenuMenager üzerinden takým deðiþtirme iþlemini yap
            menuMenager.RedTeam();
            teamSelectionPanel.SetActive(false); // Paneli kapat
        }
        else
        {
            Debug.LogError("MenuMenager bulunamadý!");
        }
    }

    void SelectBlueTeam()
    {
        if (menuMenager != null)
        {
            // Mavi takýmý seç ve MenuMenager üzerinden takým deðiþtirme iþlemini yap
            menuMenager.BlueTeam();
            teamSelectionPanel.SetActive(false); // Paneli kapat
        }
        else
        {
            Debug.LogError("MenuMenager bulunamadý!");
        }
    }
}
