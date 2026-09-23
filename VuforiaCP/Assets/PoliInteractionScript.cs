using UnityEngine;
using System.Collections;

public class PoliInteractionScript : MonoBehaviour

{
    public GameObject sapo;
    public GameObject agua;
    public Transform pontoDeRotacao;

    public void OnTrackingFound()
    {
        sapo.SetActive(true);
        agua.SetActive(true);
    }

    public void OnTrackingLost()
    {
        sapo.SetActive(false);
        agua.SetActive(false);
    }

    void Update()
    {
        if (sapo.activeSelf)
        {
            sapo.transform.RotateAround(
                pontoDeRotacao.position,
                Vector3.up,
                50f * Time.deltaTime
            );
        }
    }
}
