using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NS_UIManager
{
    public class UIManager : MonoBehaviour
    {

        // Start is called before the first frame update
        void Start()
        {

        }


        public void SetTextAmmoLoaded(int ammoLoaded)
        {
            gameObject.transform.Find("Canvas/Ammo_Info/Ammo_Loaded").GetComponent<Text>().text = ammoLoaded.ToString();
        }

        public void SetTextAmmoTotal(int ammoTotal)
        {
            gameObject.transform.Find("Canvas/Ammo_Info/Ammo_Total").GetComponent<Text>().text = ammoTotal.ToString();
        }
    }
}

