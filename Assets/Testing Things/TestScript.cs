using System.Collections.Generic;
using UnityEngine;

namespace TestingShit
{
    public class TestScript : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            var l = new List<string> {"hi", "hello"};
            string res = l.ChooseRandom();

            int i = 10;
            int ii = i.Clamp(0, 100);

            float x = Mathf.Lerp(1, 2, 0.3f);
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown("l"))
            {
                Tooltip.Show("hi");
            }

            if (Input.GetKeyUp("l"))
            {
                Tooltip.Hide();
            }
        }
    }
}
