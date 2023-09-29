using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Button : UI_Base
{
    enum Buttons
    {
        PointButton,
    }
     
    enum Texts
    {
        PointText,
        ScoreText,
    }

    enum GameObjects
    {
        TestObject,
    }

    private void Start()
    {
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));


        GetText(((int)Texts.PointText)).text = "Bind TExt";
    }

    int _score = 0;

    public void OnButtonClicked()
    {
        _score++;
    }
}
