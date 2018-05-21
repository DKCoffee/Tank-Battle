using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour {

    [SerializeField] public Button exitButton;

    void Start()
    {
        Button button = exitButton.GetComponent<Button>();
        button.onClick.AddListener(Exit);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
