using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VisowFrameWork {


    public class UIProgressBar : MonoBehaviour
    { 
        [SerializeField]
        public Image backgroundImage;
        [SerializeField]
        public Image progressImage;
        [SerializeField]

        [HideInInspector]
        protected float percent;
        [HideInInspector]
        public float Percent
        {
            get{
                return percent;
            }
            set
            {
                percent = value;
                if (progressImage == null)
                    return;
                progressImage.fillAmount = value / 100.0f;

            }
        }




        void Awake()
        {
            percent = 0;
        }


        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

