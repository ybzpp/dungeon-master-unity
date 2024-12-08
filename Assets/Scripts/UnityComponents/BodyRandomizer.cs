using UnityEngine;

public class BodyRandomizer : MonoBehaviour
{
    public GameObject[] Bodies;

    private void Start()
    {
        var randomIndex = Random.Range(0, Bodies.Length);
        for (int i = 0; i < Bodies.Length; i++)
        {
            Bodies[i].SetActive(randomIndex == i);
        }
    }
}


