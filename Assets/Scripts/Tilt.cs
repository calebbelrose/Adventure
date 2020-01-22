using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilt : MonoBehaviour
{
    public GameObject[] CargoPrefabs;

    private float weightFactor;
    private float maxWeight = 100f;
    private List<Cargo> backpack = new List<Cargo>();
    private int horizontal = 0, vertical = 0;

    private void Start()
    {
        CalculateWeightFactor();
    }

    void Update()
    {
        float random = Random.Range(-1f, 1f);
        transform.Rotate(0f, random + (transform.rotation.y + random) * weightFactor, 0f);
        transform.Rotate(0f, Input.GetAxis("Horizontal") * 0.2f, 0f);
    }

    void CalculateWeightFactor()
    {
        foreach (Transform child in transform)
            weightFactor += child.GetComponent<Cargo>().Weight * (child.position.y + child.position.z) / (maxWeight * 30);
    }

    public void NewCargo(GameObject cargo)
    {
        GameObject cargoObj = Instantiate(cargo, transform);
        backpack.Add(cargoObj.GetComponent<Cargo>());
        RearrangeCargo();
    }

    void RearrangeCargo()
    {
        Debug.Log("Test");
        backpack.Sort(delegate (Cargo c1, Cargo c2) { if (c1.Size != c2.Size) return c2.Size.CompareTo(c1.Size); else return c2.Weight.CompareTo(c1.Weight); });
        Vector3 nextPosition = Vector3.zero;

        for (int i = 0; i < backpack.Count; i++)
        {
            backpack[i].transform.SetSiblingIndex(i);

            if (vertical < 2)
            {
                float test = horizontal + backpack[i].transform.localPosition.z;
                if (test <= 4f)
                {
                    backpack[i].transform.localPosition = nextPosition;
                    if (test == 4f)
                        nextPosition = new Vector3(0f, 2f, 0f);
                    else
                        nextPosition = nextPosition + backpack[i].Dimensions;
                }
            }
            else
            {
                backpack[i].transform.localPosition = nextPosition;
                nextPosition = new Vector3(0f, nextPosition.z + backpack[i].Dimensions.z, nextPosition.y + backpack[i].Dimensions.y);
            }
        }

        CalculateWeightFactor();
    }
}