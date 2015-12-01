using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Text.RegularExpressions;
namespace ProjectK.ErgoMC.Assessment.classes
{
    class Helpers
    {

        public enum chart_type
        {
            trunk,
            arm_wrist,
            rula_final
        }
        public static double AngleBetweenTwoVectors(Vector3D vectorA, Vector3D vectorB , double minus = 0)
        {
            double dotProduct = 0.0;
            vectorA.Normalize();
            vectorB.Normalize();
            dotProduct = Vector3D.DotProduct(vectorA, vectorB);

            Vector3D crossProduct = Vector3D.CrossProduct(vectorA, vectorB);
            double crossProductLength = crossProduct.Z;
            double segmentAngle = Math.Atan2(crossProductLength, dotProduct);


              return (double)(Math.Acos(dotProduct) / Math.PI * 180) - minus;
             //return (double)Math.Acos(segmentAngle) / Math.PI * 180;
        }
        public static int Convert(String _str)
        {
            int temp = 0;
            int.TryParse(_str, out temp);
             return temp;
        }
        public static JointType GetOrigintJoint(JointType joint)
        {
            switch (joint)
            {
             //   case JointType.HandLeft:
               //     return JointType.ShoulderLeft;
                case JointType.WristRight:
                    return JointType.ShoulderRight;
            }
            return JointType.Head;
        }
        public static JointType GetParentJoint(JointType joint)
        {
            switch (joint)
            {
                case JointType.SpineBase:
                    return JointType.SpineBase;

                case JointType.Neck:
                    return JointType.SpineShoulder;

                case JointType.SpineShoulder:
                    return JointType.SpineBase;

                case JointType.ShoulderLeft:
                case JointType.ShoulderRight:
                    return JointType.SpineShoulder;

                case JointType.HipLeft:
                case JointType.HipRight:
                    return JointType.SpineBase;

                case JointType.HandTipLeft:
                case JointType.ThumbLeft:
                    return JointType.HandLeft;

                case JointType.HandTipRight:
                case JointType.ThumbRight:
                    return JointType.HandRight;
            }

            return (JointType)((int)joint - 1);
        }
        //charts values are y then x
        public static int[,] getChart(chart_type _chart)
        {
            int[,] chart;

            switch (_chart)
            {
                case chart_type.arm_wrist:
                    chart = new int[,]{
	            {1,2,2,2,2,3,3,3},
	            {2,2,2,2,3,3,3,3},
	            {2,3,3,3,3,3,4,4},
	            {2,3,3,3,3,4,4,4},
	            {3,3,3,3,3,4,4,4},
	            {3,4,4,4,4,4,5,5},
	            {3,3,4,4,4,4,5,5},
	            {3,4,4,4,4,4,5,5},
	            {4,4,4,4,4,5,5,5},
	            {4,4,4,4,4,5,5,5},
	            {4,4,4,4,4,5,5,5},
	            {4,4,4,5,5,5,6,6},
	            {5,5,5,5,5,6,6,7},
	            {5,6,6,6,6,6,7,7},
	            {6,6,6,7,7,7,7,8},
	            {7,7,7,7,7,8,8,9},
	            {8,8,8,8,8,9,9,9},
	            {9,9,9,9,9,9,9,9}

            }; return chart;

                case chart_type.trunk:
                    chart = new int[,]{{1	,3	,2	,3	,3	,4	,5	,5	,6	,6	,7	,7},
                    {2	,3	,2	,3	,4	,5	,5	,5	,6	,7	,7	,7},
                    {3	,3	,3	,4	,4	,5	,5	,6	,6	,7	,7	,7},
                    {5	,5	,5	,6	,6	,7	,7	,7	,7	,7	,8	,8},
                    {7	,7	,7	,7	,7	,8	,8	,8	,8	,8	,8	,8},
                    {8	,8	,8	,8	,8	,8	,8	,9	,9	,9	,9	,9}};
                    return chart;

                case chart_type.rula_final:

                    chart = new int[,] {{1,2,3,3,4,5,5},
                                        {2,2,3,4,4,5,5},
                                        {3,3,3,4,4,5,6},
                                        {3,3,3,4,5,6,6},
                                        {4,4,4,5,6,7,7},
                                        {4,4,5,6,6,7,7},
                                        {5,5,6,6,7,7,7},
                                        {5,5,6,7,7,7,7}};
                    return chart;
            }

            return null;
        }
        public static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-6.-]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }
        public static int getY(int L2, int L3){
            return ((L2 - 1) * 3) + (L3 - 1);
        }
        public static int getX(int L6, int L7)
        {
            return ((L6-1)*2) + (L7 -1);
        }
        public static int getYTrunk(int _neck_score)
        {
            return _neck_score -1;
        }
        public static int getXTrunk(int _trunk_score, int _leg_score)
        {
            return ((_trunk_score - 1) * 2) + (_leg_score - 1);
        }
        public static bool Check2DArray(int x, int y  , int [,] chart)
        {
            return (x < chart.GetLength(0) && y < chart.GetLength(1));
        }
    }
}
