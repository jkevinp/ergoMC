using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Text.RegularExpressions;
using System.Windows;
namespace ProjectK.ErgoMC.Assessment.classes
{
    class Helpers
    {

        public static void ToastSuccess(Window _owner, string _title, string _text, MessageBoxButton btn)
        {
            MessageBox.Show(_owner, _text, _title, btn, MessageBoxImage.Information);
        }
        public static void ToastWarning(Window _owner, string _title, string _text, MessageBoxButton btn)
        {
            MessageBox.Show(_owner, _text, _title, btn, MessageBoxImage.Warning);
        }
        public static bool  ToastWarning(Window _owner, string _title, string _text, MessageBoxButton btn , MessageBoxResult messageBoxResult)
        {
            MessageBoxResult result = MessageBox.Show(_owner, _text, _title, btn, MessageBoxImage.Warning, messageBoxResult);
            if (result == MessageBoxResult.Yes || result == MessageBoxResult.OK)
            {
                return true;
            }
            return false;
        }
        public static void ToastError(Window _owner, string _title, string _text, MessageBoxButton btn)
        {
            MessageBox.Show(_owner, _text, _title, btn, MessageBoxImage.Error);
        }
        public static void ToastRequired(Window _owner, string _title, string _text , MessageBoxButton btn)
        {
            MessageBox.Show(_owner, _text, _title, btn, MessageBoxImage.Asterisk);
        }

        public enum chart_type
        {
            trunk,
            arm_wrist,
            rula_final
        }
        public enum reba_chart_type
        {
            table_a,
            table_b,
            table_c
        }


        public static int[,] getRebaChart(reba_chart_type _type)
        {
            int[,] chart;
            switch (_type)
            {
                case reba_chart_type.table_a:
                    chart = new int[,]{{1,2,3,4,1,2,3,5,3,3,5,6}, {2,3,4,5,3,4,5,6,4,5,6,7},{2,4,5,6,4,5,6,7,5,6,7,8}, {3,5,6,7,5,6,7,8,6,7,8,9}, {4,6,7,8,6,7,8,9,7,8,9,9} };
                return chart;
                case reba_chart_type.table_b:
                    chart = new int[,]{{1,2,2,1,2,3}, {1,2,3,2,3,4},{3,4,5,4,5,5}, {4,5,5,5,6,7},{6,7,8,7,8,8}, {7,8,8,8,9,9}};
               return chart;
                case reba_chart_type.table_c:
               chart = new int[,]{{1,1,	1,	2,	3,	3,	4,	5,	6,	7,	7,	7},
                                    {1, 2,	2,	3,	4,	4,	5,	6,	6,	7,	7,	8},
                                    {2, 3,	3,	3,	4,	5,	6,	7,	7,	8,	8,	8},
                                    {3, 4,	4,	4,	5,	6,	7,	8,	8,	9,	9,	9},
                                    {4, 4,	4,	5,	6,	7,	8,	8,	9,9,9,	9,},
                                    {6, 6,	6,	7,	8,	8,	9,9,	10,	10,	10,	10},
                                    {7, 7,	7,	8,	9,  9,	9,10,	10,	11,	11,	11},
                                    {8, 8,	8,  9,	10,	10,	10,	10,	10,	11,	11,	11},
                                    {9, 9,	9,  10, 10,	10,	11,	11,	11,	12,	12,	12},
                                    {10,10,	10,	11,	11,	11,	11,	12,	12,	12,	12,	12},
                                    {11,11,	11,	11,	11,	12,	12,	12,	12,	12,	12,	12},
                                    {12,12,	12,	12,	12,	12,	12,	12,	12,	12,	12,	12}};
               return chart;
            }
            return null;

        }

        public static int getYTableA(int G67, int G68)
        {
            int x = ((G67 - 1) * 4) + (G68 - 1);
            return x;
        }
        public static int getXTableA(int D67)
        {
            return D67 - 1;
        }

        public static int getYTableB(int lower_arm_score, int wrist_score)
        {
            int x = ((lower_arm_score - 1) * 3) + (wrist_score - 1);
            return x;
        }
        public static int getXTableB(int upper_arm_score)
        {
            return upper_arm_score - 1;
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
	            {9,9,9,9,9,9,9,9}}; 
            return chart;

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
            return ( x < chart.GetLength(0) && x >= 0 && y >=0 && y < chart.GetLength(1));
        }
    }
}
