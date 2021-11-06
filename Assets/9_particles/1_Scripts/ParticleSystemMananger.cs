using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ParticleSystemMananger : MonoBehaviour
{
    [Header("Set Prefabs")]
    public List<GameObject> prefabs = new List<GameObject>();  
    private const string prefabFolderPath = "Assets/9_particles/0_Resoureces/Prefab";


    private void Update()
    {
        if (Input.GetKeyDown("r"))
            AffectParticle("boom", transform, transform.position, new Vector3(
                0, 0, Random.Range(-180.0f, 180.0f)));
    }
    public void AffectParticle(string token, Transform parent, Vector3 position, Vector3 rotation)
    {

        string name = token + "_particle";
        foreach(GameObject p in prefabs)
        {
            if(p.name == name)
            {
                var particle = Instantiate(p, position, Quaternion.Euler(rotation.x, rotation.y, rotation.z));
                particle.transform.SetParent(parent);
                StartCoroutine(DestoryParticle(particle));
            }
        }
    }
    public void AffectParticle(string token, Transform parent, Vector3 position)
    {
        string name = token + "_particle";

        GameObject particle = ObjectPooler.Instance.GetPooledObject(name);
        ParticleSystem[] ps = particle.GetComponentsInChildren<ParticleSystem>();

        particle.transform.position = parent.transform.position + position;
        particle.gameObject.SetActive(true);
        foreach(var par in ps)
        {
            par.Play();
        }
        StartCoroutine(DestoryParticle(particle));

    }
    IEnumerator DestoryParticle(GameObject obj)
    {
        yield return new WaitForSeconds(1.0f);
        obj.SetActive(false);
    }
}
