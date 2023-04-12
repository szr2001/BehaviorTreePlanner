using System;
using UnityEngine;

namespace BehaviorTreePlanner.Nodes
{
    [Serializable]
    public class NodeDesign
    {
        public string type;
        public string name;

        private float[] primarycolor = new float[3];
        private float[] secondarycollor = new float[3];

        public Color PrimaryCollor
        {
            get
            {
                return ConvertArrayToCollor(primarycolor);
            }
            private set
            {
                primarycolor = ConvertColorToArray(value);
            }
        }
        public Color SecondaryCollor 
        {
            get 
            {
                return ConvertArrayToCollor(secondarycollor);
            }
            private set
            {
                secondarycollor = ConvertColorToArray(value);
            }
        }
        public NodeDesign(string type, string name, Color primaryCollor, Color secondaryCollor)
        {
            this.type = type;
            this.name = name;
            this.SecondaryCollor = secondaryCollor;
            this.PrimaryCollor = primaryCollor;
        }


        private float[] ConvertColorToArray(Color col)
        {
            float[] colorArr = new float[3]
            {
                col.r, col.g, col.b
            };

            return colorArr;
        }
        private Color ConvertArrayToCollor(float[] arr)
        {
            Color col = new Color
                (
                    arr[0],
                    arr[1],
                    arr[2]
                );
            return col;
        }
    }
}