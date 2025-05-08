using UnityEngine.UI;

public class StartPanel : BasePanel
{
    public Button loadFileBtn;

    private void Update()
    {
        loadFileBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.openPanel<LoadFilePanel>();
        });
    }
}
