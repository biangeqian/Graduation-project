using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasMarket : MonoBehaviour
{
    public Button Mechanic;
    public Button Skier;
    public Button Therapist;
    public Button returnMenu;
    public GameObject MechanicShop;
    // Start is called before the first frame update
    void Start()
    {
        Mechanic.onClick.AddListener(goMechanic);
        Skier.onClick.AddListener(goSkier);
        Therapist.onClick.AddListener(goTherapist);
        returnMenu.onClick.AddListener(GoMainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void goMechanic()
    {
        MechanicShop.SetActive(true);
        GameManager.Instance.CanvasStack.Push(MechanicShop);
    }
    void goSkier()
    {
        
    }
    void goTherapist()
    {

    }
    void GoMainMenu()
    {
        GameObject curMenu=GameManager.Instance.CanvasStack.Pop();
        curMenu.SetActive(false);
        GameManager.Instance.MainMenu.SetActive(true);
    }
}
