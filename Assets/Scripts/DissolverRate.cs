using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolverController : MonoBehaviour
{
    [SerializeField] float dissolverRate = 0.0125f;
    [SerializeField] float refreshRate = 0.025f;
    [SerializeField] Renderer MeshRender;
    [SerializeField] Material[] Materials;
    void Start()
    {
        if (MeshRender != null)
            Materials = MeshRender.materials;
    }

    public IEnumerator DissolveCo()
    {
       if (Materials.Length > 0)
       {
           float Counter = 0;
           while (Materials[0].GetFloat("_DissolverAmount") < 1)
           {
               Counter += dissolverRate;
               for (int i = 0; i < Materials.Length; i++)
               {
                   Materials[i].SetFloat("_DissolverAmount", Counter);
                   yield return new WaitForSeconds(refreshRate);
               }
           }
       }

    }
    public void SetDissolveAmount(float amount)
    {
        for (int i = 0; i < Materials.Length; i++)
        {
            Materials[i].SetFloat("_DissolverAmount", amount);
        }
    }
}
