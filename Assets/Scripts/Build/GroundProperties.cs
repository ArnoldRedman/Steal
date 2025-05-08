using UnityEngine;

public class GroundProperties : MonoBehaviour
{
    public GroundPropertyData groundPropertyData = new GroundPropertyData();

    [HideInInspector] public GameObject InitPrefab;
    [HideInInspector] public GameObject HasBuyObj;

    private void Start()
    {
        groundPropertyData.GroundName = this.name;
        //groundPropertyData.Price = 200;
        InitPrefab = transform.Find("InitPrefab").gameObject;
    }
}
