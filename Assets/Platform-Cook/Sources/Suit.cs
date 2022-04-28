using UnityEngine;

[CreateAssetMenu(fileName = "New Suit", menuName = "Human / Suit", order = 51)]
public class Suit : ScriptableObject
{
    [SerializeField] private GameObject _skin;

    public GameObject Skin => _skin;
}