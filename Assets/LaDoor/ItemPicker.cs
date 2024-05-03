using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemPicker : MonoBehaviour
{
    public GameObject[] itemss;
    // Start is called before the first frame update
    void Start()
    {
        Choose();

    }

    // Update is called once per frame
    void Choose()
    {
        int randIndex = Random.Range(0, itemss.Length);
        GameObject selected = itemss[randIndex];
        GameObject clone = Instantiate(selected, transform.position, Quaternion.identity);
        //clone.SetActive(true);  // Objeyi aktif et

        //// Orijinal prefab'ın ölçek değerlerini koru
        //clone.transform.localScale = selected.transform.localScale;

        //// Eğer isterseniz burada ölçekleri manuel olarak da ayarlayabilirsiniz
        //// Örneğin her obje için sabit bir ölçek:
        //// clone.transform.localScale = new Vector3(1f, 1f, 1f);

        //clone.SetActive(false);  // İşlemlerden sonra objeyi tekrar pasif yap
    }
}

//public class ItemPicker : MonoBehaviour
//{
//    public GameObject[] itemss; // Editörden atayacağınız nesneler

//    void Start()
//    {
//        SpawnItems();
//    }

//    void SpawnItems()
//    {
//        foreach (var item in itemss)
//        {
//            GameObject clone = Instantiate(item, GenerateRandomPosition(), Quaternion.identity);
//            clone.SetActive(true); // Nesneyi aktif hale getir
//        }
//    }

//    Vector3 GenerateRandomPosition()
//    {
//        float x = Random.Range(-10f, 10f); // X pozisyonu için rastgele değer
//        float y = Random.Range(0f, 5f);    // Y pozisyonu için rastgele değer
//        float z = Random.Range(-10f, 10f); // Z pozisyonu için rastgele değer
//        return new Vector3(x, y, z);
//    }
//}

