using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowRequest : MonoBehaviour
{
    public TextMeshProUGUI showRequestText;
    public static ShowRequest Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
}
