using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Controls;
namespace ProjectK.ErgoMC.Assessment.classes
{
    class Helpers
    {




        public static void addJointLeg(List<Tuple<JointType, JointType>> bones)
        {

            // Right Leg
           bones.Add(new Tuple<JointType, JointType>(JointType.HipRight, JointType.KneeRight));
           bones.Add(new Tuple<JointType, JointType>(JointType.KneeRight, JointType.AnkleRight));
           bones.Add(new Tuple<JointType, JointType>(JointType.AnkleRight, JointType.FootRight));

           bones.Add(new Tuple<JointType, JointType>(JointType.HipLeft, JointType.KneeLeft));
           bones.Add(new Tuple<JointType, JointType>(JointType.KneeLeft, JointType.AnkleLeft));
           bones.Add(new Tuple<JointType, JointType>(JointType.AnkleLeft, JointType.FootLeft));
        }
        public static void addJointTorso(List<Tuple<JointType, JointType>> bones)
        {
            // Torso
            bones.Add(new Tuple<JointType, JointType>(JointType.Head, JointType.Neck));
            bones.Add(new Tuple<JointType, JointType>(JointType.Neck, JointType.SpineShoulder));
            bones.Add(new Tuple<JointType, JointType>(JointType.SpineShoulder, JointType.SpineMid));
            bones.Add(new Tuple<JointType, JointType>(JointType.SpineMid, JointType.SpineBase));
            bones.Add(new Tuple<JointType, JointType>(JointType.SpineShoulder, JointType.ShoulderRight));
            bones.Add(new Tuple<JointType, JointType>(JointType.SpineShoulder, JointType.ShoulderLeft));
            bones.Add(new Tuple<JointType, JointType>(JointType.SpineBase, JointType.HipRight));
            bones.Add(new Tuple<JointType, JointType>(JointType.SpineBase, JointType.HipLeft));

        }
        public static void addJointArm(List<Tuple<JointType, JointType>> bones)
        {
            // Right Arm

            bones.Add(new Tuple<JointType, JointType>(JointType.HipRight, JointType.ShoulderRight));
            bones.Add(new Tuple<JointType, JointType>(JointType.ShoulderRight, JointType.ElbowRight));
            bones.Add(new Tuple<JointType, JointType>(JointType.ElbowRight, JointType.WristRight));

            bones.Add(new Tuple<JointType, JointType>(JointType.HipRight, JointType.Neck));
            // this.bones.Add(new Tuple<JointType, JointType>(JointType.HipRight, JointType.Neck));


            // this.bones.Add(new Tuple<JointType, JointType>(JointType.ShoulderRight, JointType.ElbowRight));
            //this.bones.Add(new Tuple<JointType, JointType>(JointType.ElbowRight, JointType.WristRight));
            //this.bones.Add(new Tuple<JointType, JointType>(JointType.WristRight, JointType.HandRight));
            //this.bones.Add(new Tuple<JointType, JointType>(JointType.HandRight, JointType.HandTipRight));
            //this.bones.Add(new Tuple<JointType, JointType>(JointType.WristRight, JointType.ThumbRight));

            // Left Arm
            //this.bones.Add(new Tuple<JointType, JointType>(JointType.ShoulderLeft, JointType.ElbowLeft));
            //this.bones.Add(new Tuple<JointType, JointType>(JointType.ElbowLeft, JointType.WristLeft));
            //this.bones.Add(new Tuple<JointType, JointType>(JointType.WristLeft, JointType.HandLeft));
            //this.bones.Add(new Tuple<JointType, JointType>(JointType.HandLeft, JointType.HandTipLeft));
            //this.bones.Add(new Tuple<JointType, JointType>(JointType.WristLeft, JointType.ThumbLeft));
        }








       public static RulaObject HandleEvent(object sender, RoutedEventArgs e, RulaObject rulaObject)
        {
            string _contentText = Helpers.GetContentText(sender);
            Type _senderType = sender.GetType();
            string _type = "";

            if (sender.GetType().Name == "CheckBox")
            {
                _type = "Additional";
            }
            else if (sender.GetType().Name == "RadioButton")
            {
                _type = "Score";
            }

            var rdb = sender as System.Windows.Controls.Control;
            if (rdb.Tag == null) return rulaObject;
            String _tag = rdb.Tag.ToString();
            string _group = rdb.Uid.ToString();
            switch (_group)
            {
                case "upper_arm":
                    if (_type == "Additional")
                    {
                        rulaObject.score_upper_arm.currentAdditionalChoices.Add(new IndexScoreChoice(Helpers.Convert(_tag), _contentText , _type));
                        rulaObject.score_upper_arm.AdditionalScore += (Helpers.Convert(_tag));
                    }
                    else if (_type == "Score")
                    {
                        rulaObject.score_upper_arm.Score = (Helpers.Convert(_tag));
                        rulaObject.score_neck_trunk_legs_load.currentAdditionalChoices.Add(new IndexScoreChoice(Helpers.Convert(_tag), _contentText, _type));
                        
                    }
                    break;
                case "lower_arm":
                    if (_type == "Additional")
                    {
                        rulaObject.score_lower_arm.currentAdditionalChoices.Add(new IndexScoreChoice(Helpers.Convert(_tag), _contentText , _type));
                        rulaObject.score_lower_arm.AdditionalScore += (Helpers.Convert(_tag));
                    }
                    else if (_type == "Score")
                    {
                        rulaObject.score_lower_arm.Score = (Helpers.Convert(_tag));
                        rulaObject.score_neck_trunk_legs_load.currentAdditionalChoices.Add(new IndexScoreChoice(Helpers.Convert(_tag), _contentText, _type));
                        
                    }
                    break;
                case "wrist_position":
                    if (_type == "Additional")
                    {
                        rulaObject.score_wrist_position.currentAdditionalChoices.Add(new IndexScoreChoice(Helpers.Convert(_tag), _contentText , _type));
                        rulaObject.score_wrist_position.AdditionalScore += (Helpers.Convert(_tag));
                    }
                    else if (_type == "Score")
                    {
                        rulaObject.score_wrist_position.Score = (Helpers.Convert(_tag));
                        rulaObject.score_neck_trunk_legs_load.currentAdditionalChoices.Add(new IndexScoreChoice(Helpers.Convert(_tag), _contentText, _type));
                        
                    }
                    break;
                case "wrist_twist":
                    if (_type == "Additional")
                    {
                        rulaObject.score_wrist_twist.currentAdditionalChoices.Add(new IndexScoreChoice(Helpers.Convert(_tag), _contentText , _type));
                        rulaObject.score_wrist_twist.AdditionalScore += (Helpers.Convert(_tag));
                    }
                    else if (_type == "Score")
                    {
                        rulaObject.score_wrist_twist.Score = (Helpers.Convert(_tag));
                        rulaObject.score_neck_trunk_legs_load.currentAdditionalChoices.Add(new IndexScoreChoice(Helpers.Convert(_tag), _contentText, _type));
                        
                    }
                    break;
                case "arm_wrist_muscle":
                    if (_type == "Additional")
                    {
                        rulaObject.score_arm_wrist_muscle.currentAdditionalChoices.Add(new IndexScoreChoice(Helpers.Convert(_tag), _contentText , _type));
                        rulaObject.score_arm_wrist_muscle.AdditionalScore += (Helpers.Convert(_tag));

                    }
                    else if (_type == "Score")
                    {
                        rulaObject.score_arm_wrist_muscle.Score = (Helpers.Convert(_tag));
                        rulaObject.score_neck_trunk_legs_load.currentAdditionalChoices.Add(new IndexScoreChoice(Helpers.Convert(_tag), _contentText, _type));
                        
                    }

                    break;
                case "arm_wrist_load":
                    if (_type == "Additional")
                    {
                        rulaObject.score_arm_wrist_load.currentAdditionalChoices.Add(new IndexScoreChoice(Helpers.Convert(_tag), _contentText , _type));
                        rulaObject.score_arm_wrist_load.AdditionalScore += (Helpers.Convert(_tag));

                    }
                    else if (_type == "Score")
                    {
                        rulaObject.score_arm_wrist_load.Score = (Helpers.Convert(_tag));
                        rulaObject.score_neck_trunk_legs_load.currentAdditionalChoices.Add(new IndexScoreChoice(Helpers.Convert(_tag), _contentText, _type));
                        
                    }
                    break;
                case "neck_position":
                    if (_type == "Additional")
                    {
                        rulaObject.score_neck.currentAdditionalChoices.Add(new IndexScoreChoice(Helpers.Convert(_tag), _contentText , _type));
                        rulaObject.score_neck.AdditionalScore += (Helpers.Convert(_tag));
                        rulaObject.score_neck_trunk_legs_load.currentAdditionalChoices.Add(new IndexScoreChoice(Helpers.Convert(_tag), _contentText, _type));
                        
                    }
                    else if (_type == "Score")
                    {
                        rulaObject.score_neck.Score = (Helpers.Convert(_tag));
                        rulaObject.score_neck_trunk_legs_load.currentAdditionalChoices.Add(new IndexScoreChoice(Helpers.Convert(_tag), _contentText, _type));
                        
                    }
                    break;
                case "trunk_position":
                    if (_type == "Additional")
                    {
                        rulaObject.score_trunk.currentAdditionalChoices.Add(new IndexScoreChoice(Helpers.Convert(_tag), _contentText , _type));
                        rulaObject.score_trunk.AdditionalScore += (Helpers.Convert(_tag));
                    }
                    else if (_type == "Score")
                    {
                        rulaObject.score_trunk.Score = (Helpers.Convert(_tag));
                        rulaObject.score_neck_trunk_legs_load.currentAdditionalChoices.Add(new IndexScoreChoice(Helpers.Convert(_tag), _contentText, _type));
                        

                    }
                    break;
                case "legs_position":
                    if (_type == "Additional")
                    {
                        rulaObject.score_legs.currentAdditionalChoices.Add(new IndexScoreChoice(Helpers.Convert(_tag), _contentText , _type));
                        rulaObject.score_legs.AdditionalScore += (Helpers.Convert(_tag));
                    }
                    else if (_type == "Score")
                    {
                        rulaObject.score_legs.Score = (Helpers.Convert(_tag));
                        rulaObject.score_neck_trunk_legs_load.currentAdditionalChoices.Add(new IndexScoreChoice(Helpers.Convert(_tag), _contentText, _type));
                        
                    }
                    break;
                case "neck_trunk_legs_muscle":
                    if (_type == "Additional")
                    {
                        rulaObject.score_neck_trunk_legs_muscle.currentAdditionalChoices.Add(new IndexScoreChoice(Helpers.Convert(_tag), _contentText , _type));
                        rulaObject.score_neck_trunk_legs_muscle.AdditionalScore += (Helpers.Convert(_tag));

                    }
                    else if (_type == "Score")
                    {
                        rulaObject.score_neck_trunk_legs_muscle.Score = (Helpers.Convert(_tag));
                        rulaObject.score_neck_trunk_legs_load.currentAdditionalChoices.Add(new IndexScoreChoice(Helpers.Convert(_tag), _contentText, _type));
                        
                    }
                    break;
                case "neck_trunk_legs_load":
                    if (_type == "Additional")
                    {
                        rulaObject.score_neck_trunk_legs_load.currentAdditionalChoices.Add(new IndexScoreChoice(Helpers.Convert(_tag), _contentText , _type));
                        rulaObject.score_neck_trunk_legs_load.AdditionalScore += (Helpers.Convert(_tag));
                    }
                    else if (_type == "Score")
                    {
                        rulaObject.score_neck_trunk_legs_load.Score = (Helpers.Convert(_tag));
                        rulaObject.score_neck_trunk_legs_load.currentAdditionalChoices.Add(new IndexScoreChoice(Helpers.Convert(_tag), _contentText, _type));
                        
                    }
                    break;
            }
            return rulaObject;
        }
       public static RebaObject HandleEvent(object sender, RoutedEventArgs e, RebaObject rebaObject)
       {
            string _contentText = Helpers.GetContentText(sender);
            Type _senderType = sender.GetType();
            string _type = "";

            if (sender.GetType().Name == "CheckBox")
            {
                _type = "Additional";
            }
            else if (sender.GetType().Name == "RadioButton")
            {
                _type = "Score";
            }
            var rdb = sender as System.Windows.Controls.Control;
            if (rdb.Tag == null) return rebaObject;
            String _tag = rdb.Tag.ToString();
            string _group = rdb.Uid.ToString();
            switch (_group)
            {
                case "upper_arm":
                    if (_type == "Additional")
                    {
                        rebaObject.score_upper_arm.currentAdditionalChoices.Add(new IndexScoreChoice(Helpers.Convert(_tag), _contentText , _type));
                        rebaObject.score_upper_arm.AdditionalScore += (Helpers.Convert(_tag));
                    }
                    else if (_type == "Score")
                    {
                        rebaObject.score_upper_arm.currentAdditionalChoices.Add(new IndexScoreChoice(Helpers.Convert(_tag), _contentText , _type));
                        rebaObject.score_upper_arm.Score = (Helpers.Convert(_tag));
                    }
                    break;
                case "lower_arm":
                    if (_type == "Additional")
                    {
                        rebaObject.score_upper_arm.currentAdditionalChoices.Add(new IndexScoreChoice(Helpers.Convert(_tag), _contentText, _type)); 
                        rebaObject.score_lower_arm.AdditionalScore += (Helpers.Convert(_tag));
                    }
                    else if (_type == "Score")
                    {
                        rebaObject.score_upper_arm.currentAdditionalChoices.Add(new IndexScoreChoice(Helpers.Convert(_tag), _contentText, _type));
                        rebaObject.score_lower_arm.Score = (Helpers.Convert(_tag));
                    }
                    break;
                case "wrist_position":
                    if (_type == "Additional")
                    {
                        rebaObject.score_upper_arm.currentAdditionalChoices.Add(new IndexScoreChoice(Helpers.Convert(_tag), _contentText, _type)); rebaObject.score_wrist_position.AdditionalScore += (Helpers.Convert(_tag));
                    }
                    else if (_type == "Score")
                    {
                        rebaObject.score_wrist_position.Score = (Helpers.Convert(_tag));
                        rebaObject.score_upper_arm.currentAdditionalChoices.Add(new IndexScoreChoice(Helpers.Convert(_tag), _contentText, _type));
                    }
                    break;
                
                case "neck_position":
                    if (_type == "Additional")
                    {
                        rebaObject.score_upper_arm.currentAdditionalChoices.Add(new IndexScoreChoice(Helpers.Convert(_tag), _contentText, _type)); 
                        rebaObject.score_neck.AdditionalScore += (Helpers.Convert(_tag));
                    }
                    else if (_type == "Score")
                    {
                        rebaObject.score_neck.Score = (Helpers.Convert(_tag));
                        rebaObject.score_upper_arm.currentAdditionalChoices.Add(new IndexScoreChoice(Helpers.Convert(_tag), _contentText, _type));
                    }
                    break;
                case "trunk_position":
                    if (_type == "Additional")
                    {
                        rebaObject.score_upper_arm.currentAdditionalChoices.Add(new IndexScoreChoice(Helpers.Convert(_tag), _contentText, _type));
                        rebaObject.score_trunk.AdditionalScore += (Helpers.Convert(_tag));
                    }
                    else if (_type == "Score")
                    {
                        rebaObject.score_upper_arm.currentAdditionalChoices.Add(new IndexScoreChoice(Helpers.Convert(_tag), _contentText, _type));
                        rebaObject.score_trunk.Score = (Helpers.Convert(_tag));

                    }
                    break;
                case "legs_position":
                    if (_type == "Additional")
                    {
                        rebaObject.score_upper_arm.currentAdditionalChoices.Add(new IndexScoreChoice(Helpers.Convert(_tag), _contentText, _type));
                        rebaObject.score_legs.AdditionalScore += (Helpers.Convert(_tag));
                    }
                    else if (_type == "Score")
                    {
                        rebaObject.score_upper_arm.currentAdditionalChoices.Add(new IndexScoreChoice(Helpers.Convert(_tag), _contentText, _type));
                        rebaObject.score_legs.Score = (Helpers.Convert(_tag));
                    }
                    break;

                case "neck_trunk_legs_load":
                    if (_type == "Additional")
                    {
                        rebaObject.score_upper_arm.currentAdditionalChoices.Add(new IndexScoreChoice(Helpers.Convert(_tag), _contentText, _type));
                        rebaObject.score_neck_trunk_legs_load.AdditionalScore += (Helpers.Convert(_tag));
                    }
                    else if (_type == "Score")
                    {
                        rebaObject.score_neck_trunk_legs_load.Score = (Helpers.Convert(_tag));
                        rebaObject.score_upper_arm.currentAdditionalChoices.Add(new IndexScoreChoice(Helpers.Convert(_tag), _contentText, _type));
                    }
                    break;
                case "activity":
                    if (_type == "Additional")
                    {
                        rebaObject.score_activity.currentAdditionalChoices.Add(new IndexScoreChoice(Helpers.Convert(_tag), _contentText , _type));
                        rebaObject.score_activity.AdditionalScore += (Helpers.Convert(_tag));
                    }
                    else if (_type == "Score")
                    {
                        rebaObject.score_activity.Score = (Helpers.Convert(_tag));
                        rebaObject.score_upper_arm.currentAdditionalChoices.Add(new IndexScoreChoice(Helpers.Convert(_tag), _contentText, _type));
                    }
                    break;
                case "coupling":
                    if (_type == "Additional")
                    {
                        rebaObject.score_coupling.currentAdditionalChoices.Add(new IndexScoreChoice(Helpers.Convert(_tag), _contentText, _type));
                        rebaObject.score_coupling.AdditionalScore += (Helpers.Convert(_tag));
                    }
                    else if (_type == "Score")
                    {
                        rebaObject.score_coupling.Score = (Helpers.Convert(_tag));
                        rebaObject.score_upper_arm.currentAdditionalChoices.Add(new IndexScoreChoice(Helpers.Convert(_tag), _contentText, _type));
                    }
                    break;
            }
            return rebaObject;
        }
       public static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }
       public static string GetContentText(object sender)
       {
           string _contentText = string.Empty;
           switch (sender.GetType().Name)
           {
               case "CheckBox":
                   _contentText = (sender as CheckBox).Content.ToString();
                   break;
               case "RadioButton":
                   _contentText = (sender as RadioButton).Content.ToString();
                   break;
               case "TextBox":
                   _contentText = (sender as TextBox).Text.ToString();
                   break;
               default:

                   break;
           }
           return _contentText;
       }
       public static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        public static void ToastSuccess(Window _owner, string _title, string _text, MessageBoxButton btn)
        {
            MessageBox.Show(_owner, _text, _title, btn, MessageBoxImage.Information);
        }
        public static void ToastWarning(Window _owner, string _title, string _text, MessageBoxButton btn)
        {
            MessageBox.Show(_owner, _text, _title, btn, MessageBoxImage.Warning);
        }
        public static bool ToastWarning(Window _owner, string _title, string _text, MessageBoxButton btn , MessageBoxResult messageBoxResult)
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
        public static int GetRulaLowerArmScore(double lower_arm_angle)
        {
            int score_lower_armk = 0;
            if (lower_arm_angle >= 60 && lower_arm_angle < 100)
            {
                score_lower_armk = 1;
            }
            else if (lower_arm_angle < 60 || lower_arm_angle > 100)
            {
                score_lower_armk = 2;
            }
            else
            {
                score_lower_armk = 0;
            }
            return score_lower_armk;
        }
        public static int GetRulaTrunkScore(double trunk_angle)
        {
            if (trunk_angle == 0)
            {
                return 1;
            }
            else if (trunk_angle >= 0 && trunk_angle < 20)
            {
                return 2;
            }
            else if (trunk_angle >= 20 && trunk_angle < 60)
            {
                return 3;
            }
            else if (trunk_angle >= 60)
            {
                return 4;
            }
            return 0;
        }

        public static int GetRebaTrunkScore(double trunk_angle)
        {
            if (trunk_angle == 0)
            {
                return 1;
            }
            else if (trunk_angle > -20 && trunk_angle < 20)
            {
                return 2;
            }
            else if (trunk_angle >= 20 && trunk_angle < 60)
            {
                return 3;
            }
            else if (trunk_angle >= 60)
            {
                return 4;
            }
            return 0;
        }
        public static int GetRebaNeckScore(double neck_angle)
        {
            if (neck_angle >= 0 && neck_angle < 20)
            {
                return 1;
            }
            else if (neck_angle >= 20)
            {
                return 2;
            }
            else if (neck_angle <= 0)
            {
                return 2;
            }
            
            return 0;
        }
        public static int GetRulaNeckScore(double neck_angle)
        {
            if (neck_angle >= 0 && neck_angle < 10)
            {
                return 1;
            }
            else if (neck_angle >= 10 && neck_angle < 20)
            {
                return 2;
            }
            else if (neck_angle >= 20)
            {
                return 3;
            }
            else if (neck_angle < 0)
            {
                return 4;
            }
            return 0;
        }

        public static int GetRulaUpperArmScore(double upper_arm_angle)
        {
            int score_upper_armk = 0;
            if (upper_arm_angle >= -20 && upper_arm_angle < 20)
            {
                score_upper_armk = 1;
            }
            else if (upper_arm_angle < -20)
            {
                score_upper_armk = 2;
            }
            else if (upper_arm_angle > 20 && upper_arm_angle < 45)
            {
                score_upper_armk = 2;
            }
            else if (upper_arm_angle > 45 && upper_arm_angle < 90)
            {
                score_upper_armk = 3;
            }
            else if (upper_arm_angle > 90)
            {
                score_upper_armk = 4;
            }
            else
            {
                score_upper_armk = 0;
            }
            return score_upper_armk;
        }


        public static double Inverse(double _d)
        {
            return _d * -1;
        }
        public static double Angle(CameraSpacePoint v1, CameraSpacePoint v2, double offsetInDegrees = 0.0)
        {
            return (RadianToDegree(Math.Atan2(-v2.Y + v1.Y, -v2.X + v1.X)) + offsetInDegrees % 180.0);
        }
        public static double RadianToDegree(double radian)
        {
            var degree = radian * (180 / Math.PI);
            //if (degree < 0) degree = 360 + degree;
            return degree;
        }

        public static void Animate(Window _this, float _time, float from, float to, DependencyProperty property)
        {
            DoubleAnimation da = new DoubleAnimation(from, to, new Duration(TimeSpan.FromSeconds(_time)));
            _this.BeginAnimation(property, da);
            //da.Completed += (s, e) =>
            //{

            //};
        }

        public static void Animate(Page _this, float _time, float from, float to, DependencyProperty property)
        {
            DoubleAnimation da = new DoubleAnimation(from, to, new Duration(TimeSpan.FromSeconds(_time)));
            _this.BeginAnimation(property, da);
            
        }
        public static void Animate(Page _this, float _time, float from, float to, DependencyProperty property , EventHandler eh)
        {
            DoubleAnimation da = new DoubleAnimation(from, to, new Duration(TimeSpan.FromSeconds(_time)));
            _this.BeginAnimation(property, da);

            da.Completed += eh;
        }
        public static void Animate(Window _this, float _time, float from, float to, DependencyProperty property , bool isClosing)
        {
            DoubleAnimation da = new DoubleAnimation(from, to, new Duration(TimeSpan.FromSeconds(_time)));
            da.Completed += (s, e) =>
            {
               
               _this.Close();
            };
            _this.BeginAnimation(property, da);
          
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
