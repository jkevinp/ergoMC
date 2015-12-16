namespace ProjectK.ErgoMC.Assessment
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using Microsoft.Kinect;
    using ProjectK.ErgoMC.Assessment.classes;
    using System.Windows.Media.Media3D;
    using ProjectK.ErgoMC.Assessment.Library;
    using ProjectK.ErgoMC.Assessment.UI;
    using System.Windows.Controls;
    /// <summary>
    /// Interaction logic for MainWindow
    /// </summary>
    public partial class Reba : Page, INotifyPropertyChanged
    {
        private RebaObject rebaObject = new RebaObject();
        public RebaObject RebaObject
        {
            get
            {
                return this.rebaObject;
            }
            set
            {
                this.rebaObject = value;
            }
        }
        private string typeOfEvaluation = "right";
        public string TypeOfEvaluation
        {
            get { return this.typeOfEvaluation; }
        }

        /// <summary>
        /// Radius of drawn hand circles
        /// </summary>
        private const double HandSize = 30;
        /// <summary>
        /// Thickness of drawn joint lines
        /// </summary>
        private const double JointThickness = 10;
        /// <summary>
        /// Thickness of clip edge rectangles
        /// </summary>
        private const double ClipBoundsThickness = 10;
        /// <summary>
        /// Constant for clamping Z values of camera space points from being negative
        /// </summary>
        private const float InferredZPositionClamp = 0.1f;
        /// <summary>
        /// Brush used for drawing hands that are currently tracked as closed
        /// </summary>
        private readonly Brush handClosedBrush = new SolidColorBrush(Color.FromArgb(128, 255, 0, 0));
        /// <summary>
        /// Brush used for drawing hands that are currently tracked as opened
        /// </summary>
        private readonly Brush handOpenBrush = new SolidColorBrush(Color.FromArgb(128, 0, 255, 0));
        /// <summary>
        /// Brush used for drawing hands that are currently tracked as in lasso (pointer) position
        /// </summary>
        private readonly Brush handLassoBrush = new SolidColorBrush(Color.FromArgb(128, 0, 0, 255));
        /// <summary>
        /// Brush used for drawing joints that are currently tracked
        /// </summary>
        private readonly Brush trackedJointBrush = new SolidColorBrush(Color.FromArgb(255, 68, 192, 68));
        private readonly Brush textJointBrush = new SolidColorBrush(Color.FromArgb(255, 68, 192, 68));
        /// <summary>
        /// Brush used for drawing joints that are currently inferred
        /// </summary>        
        private readonly Brush inferredJointBrush = Brushes.Yellow;
        /// <summary>
        /// Pen used for drawing bones that are currently inferred
        /// </summary>        
        private readonly Pen inferredBonePen = new Pen(Brushes.Gray, 1);
        /// <summary>
        /// Drawing group for body rendering output
        /// </summary>
        private DrawingGroup drawingGroup;
        /// <summary>
        /// Drawing image that we will display
        /// </summary>
        private DrawingImage imageSource;
        /// <summary>
        /// Active Kinect sensor
        /// </summary>
        private KinectSensor kinectSensor = null;
        /// <summary>
        /// Coordinate mapper to map one type of point to another
        /// </summary>
        private CoordinateMapper coordinateMapper = null;
        /// <summary>
        /// Reader for body frames
        /// </summary>
        private BodyFrameReader bodyFrameReader = null;
        private ColorFrameReader colorFrameReader = null;

        WriteableBitmap bitmap;
        /// <summary>
        /// Array for the bodies
        /// </summary>
        private Body[] bodies = null;
        /// <summary>
        /// definition of bones
        /// </summary>
        private List<Tuple<JointType, JointType>> bones;
        /// <summary>
        /// Width of display (depth space)
        /// </summary>
        private int displayWidth;
        /// <summary>
        /// Height of display (depth space)
        /// </summary>
        private int displayHeight;
        /// <summary>
        /// List of colors for each body tracked
        /// </summary>
        private List<Pen> bodyColors;
        /// <summary>
        /// Current status text to display
        /// </summary>
        private string statusText = null;

           public ImageSource BodyImageSource
        {
            get
            {
                return this.bitmap;
            }
        }
        public enum ScanType
        {
            UpperBody,
            LowerBody,
            Arm,
            All
        }
        private ScanType scanType = ScanType.All;
        public void init()
        {
            this.RebaObject = rebaObject;
    

            this.kinectSensor = KinectSensor.GetDefault();
            this.coordinateMapper = this.kinectSensor.CoordinateMapper;
            FrameDescription frameDescription = this.kinectSensor.DepthFrameSource.FrameDescription;
            FrameDescription colorFrameDescription = this.kinectSensor.ColorFrameSource.FrameDescription;
            this.displayWidth = colorFrameDescription.Width;
            this.displayHeight = colorFrameDescription.Height;
            this.bitmap = new WriteableBitmap(colorFrameDescription.Width, colorFrameDescription.Height, 96.0, 96.0, PixelFormats.Bgr32, null);
            this.bodyFrameReader = this.kinectSensor.BodyFrameSource.OpenReader();
            this.colorFrameReader = this.kinectSensor.ColorFrameSource.OpenReader();
            if (this.bodyFrameReader != null) this.bodyFrameReader.FrameArrived += this.Reader_FrameArrived;
            if (this.colorFrameReader != null) this.colorFrameReader.FrameArrived += this.ColorFrameReaderFrameArrived;
            this.bones = new List<Tuple<JointType, JointType>>();
            switch (scanType)
            {
                case ScanType.All:
                    Helpers.addJointTorso(this.bones);
                    Helpers.addJointArm(this.bones);
                    Helpers.addJointLeg(this.bones);
                    break;
                case ScanType.Arm:
                    Helpers.addJointArm(this.bones);
                    break;
                case ScanType.LowerBody:
                    Helpers.addJointLeg(this.bones);
                    break;
                case ScanType.UpperBody:
                    Helpers.addJointArm(this.bones);
                    break;
                default:
                    Helpers.addJointTorso(this.bones);
                    Helpers.addJointArm(this.bones);
                    Helpers.addJointLeg(this.bones);
                    break;
            }
            this.bodyColors = new List<Pen>();
            this.bodyColors.Add(new Pen(Brushes.Red, 6));
            this.bodyColors.Add(new Pen(Brushes.Orange, 6));
            this.bodyColors.Add(new Pen(Brushes.Green, 6));
            this.bodyColors.Add(new Pen(Brushes.Blue, 6));
            this.bodyColors.Add(new Pen(Brushes.Indigo, 6));
            this.bodyColors.Add(new Pen(Brushes.Violet, 6));

            this.StatusText = this.kinectSensor.IsAvailable ? ProjectK.ErgoMC.Assessment.Properties.Resources.RunningStatusText : ProjectK.ErgoMC.Assessment.Properties.Resources.NoSensorStatusText;
            this.drawingGroup = new DrawingGroup();
            this.imageSource = new DrawingImage(this.drawingGroup);
            this.kinectSensor.IsAvailableChanged += this.Sensor_IsAvailableChanged;
            this.kinectSensor.OpenMultiSourceFrameReader(FrameSourceTypes.Body | FrameSourceTypes.BodyIndex | FrameSourceTypes.Color | FrameSourceTypes.Depth);
            this.kinectSensor.Open();

            this.DataContext = this;
        }
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public Reba()
        {
            Modal m = new Modal("Please select one", "Which side do you want to evaluate", "right", "left");
            var x = m.ShowDialog();
            if (x.Value)
            {
                this.typeOfEvaluation = "right";
            }
            else
            {
                this.typeOfEvaluation = "left";
            }
            init();
            this.InitializeComponent();
        }
        private Employee _employee = null;
        public Reba(Employee _emp , string _type)
        {
            typeOfEvaluation = _type;

            init();
            this.InitializeComponent();
            
            this._employee = _emp;
        }
        /// <summary>
        /// INotifyPropertyChangedPropertyChanged event to allow window controls to bind to changeable data
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Gets the bitmap to display
        /// </summary>
        public ImageSource ImageSource
        {
            get
            {
                return this.imageSource;
            }
        }
        /// <summary>
        /// Gets or sets the current status text to display
        /// </summary>
        public string StatusText
        {
            get
            {
                return this.statusText;
            }

            set
            {
                if (this.statusText != value)
                {
                    this.statusText = value;

                    // notify any bound elements that the text has changed
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("StatusText"));
                    }
                }
            }
        }
        /// <summary>
        /// Execute start up tasks
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.bodyFrameReader != null)
            {
                this.bodyFrameReader.FrameArrived += this.Reader_FrameArrived;
            }
            Helpers.Animate(this, 0.3f, 0, (float)this.ActualWidth, Page.WidthProperty );
            Helpers.Animate(this, 0.3f, 0, 1f, Page.OpacityProperty);
        }
        /// <summary>
        /// Execute shutdown tasks
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (this.bodyFrameReader != null)
            {
                // BodyFrameReader is IDisposable
                this.bodyFrameReader.Dispose();
                this.bodyFrameReader = null;
            }
              if (this.colorFrameReader != null)
            {
                this.colorFrameReader.Dispose();
                this.colorFrameReader = null;
            }
 

            if (this.kinectSensor != null)
            {
                this.kinectSensor.Close();
                this.kinectSensor = null;
            }
        }

          int score_upper_armk;
        int score_lower_armk;
        int score_trunk;
        int score_neck;


        /// <summary>
        /// Handles the body frame data arriving from the sensor
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void Reader_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            bool dataReceived = false;

            using (BodyFrame bodyFrame = e.FrameReference.AcquireFrame())
            {
                if (bodyFrame != null)
                {
                    if (this.bodies == null)
                    {
                        this.bodies = new Body[bodyFrame.BodyCount];
                    }

                    // The first time GetAndRefreshBodyData is called, Kinect will allocate each Body in the array.
                    // As long as those body objects are not disposed and not set to null in the array,
                    // those body objects will be re-used.
                    bodyFrame.GetAndRefreshBodyData(this.bodies);
                    dataReceived = true;
                }
            }

            if (dataReceived)
            {
                txt_kinect_info.Content = "Tracking: " + this.scanType.ToString() + "\n";
                
                if(this.drawingGroup != null && this.bodyColors != null)
                using (DrawingContext dc = this.drawingGroup.Open())
                {
                    // Draw a transparent background to set the render size
                    dc.DrawRectangle(Brushes.Transparent, null, new Rect(0.0, 0.0, this.displayWidth, this.displayHeight));
                     int penIndex = 0;
                    foreach (Body body in this.bodies)
                    {
                        Pen drawPen = this.bodyColors[penIndex++];

                        if (body.IsTracked)//body.istrack
                        {
                            this.DrawClippedEdges(body, dc);
                            IReadOnlyDictionary<JointType, Joint> joints = body.Joints;
                            Dictionary<JointType, Point> jointPoints = new Dictionary<JointType, Point>();

                            CameraSpacePoint r_hip = new CameraSpacePoint();
                            CameraSpacePoint r_shoulder = new CameraSpacePoint();
                            CameraSpacePoint r_elbow = new CameraSpacePoint();
                            CameraSpacePoint r_wrist = new CameraSpacePoint();
                            CameraSpacePoint l_hip = new CameraSpacePoint();
                            CameraSpacePoint l_shoulder = new CameraSpacePoint();
                            CameraSpacePoint l_elbow = new CameraSpacePoint();
                            CameraSpacePoint l_wrist = new CameraSpacePoint();
                            CameraSpacePoint neck = new CameraSpacePoint();
                            CameraSpacePoint up = new CameraSpacePoint();
                            CameraSpacePoint spine_base = new CameraSpacePoint();
                            CameraSpacePoint spine_mid = new CameraSpacePoint();
                            CameraSpacePoint head = new CameraSpacePoint();

                            foreach (JointType jointType in joints.Keys)
                            {
                                CameraSpacePoint position = joints[jointType].Position;
                                float x = position.X;
                                switch (jointType)
                                {
                                    case JointType.ShoulderRight:
                                        r_shoulder.X = position.X;
                                        r_shoulder.Y = position.Y;
                                        break;
                                    case JointType.HipRight:
                                        r_hip.X = position.X;
                                        r_hip.Y = position.Y;
                                        //this is for trunk
                                        up.X = position.X;
                                        up.Y = position.Y + 0.1f;
                                        break;
                                    case JointType.ElbowRight:
                                        r_elbow.X = position.X;
                                        r_elbow.Y = position.Y;
                                        break;
                                    case JointType.WristRight:
                                        r_wrist.X = position.X;
                                        r_wrist.Y = position.Y;
                                        break;

                                    case JointType.ShoulderLeft:
                                        l_shoulder.X = position.X;
                                        l_shoulder.Y = position.Y;
                                        break;
                                    case JointType.HipLeft:
                                        l_hip.X = position.X;
                                        l_hip.Y = position.Y;
                                        //this is for trunk
                                        up.X = position.X;
                                        up.Y = position.Y + 0.1f;
                                        break;
                                    case JointType.ElbowLeft:
                                        l_elbow.X = position.X;
                                        l_elbow.Y = position.Y;
                                        break;
                                    case JointType.WristLeft:
                                        l_wrist.X = position.X;
                                        l_wrist.Y = position.Y;
                                        break;


                                    case JointType.Neck:
                                        neck.X = position.X;
                                        neck.Y = position.Y;
                                     break;
                                    case JointType.SpineBase:
                                     spine_base.X = position.X;
                                     spine_base.Y = position.Y;
                                     break;
                                    case JointType.Head:
                                     head.X = position.X;
                                     head.Y = position.Y;
                                     break;
                                    case JointType.SpineMid:
                                     spine_mid.X = position.X;
                                     spine_mid.Y = position.Y;
                                     break;
                                }
                                if (position.Z < 0) position.Z = InferredZPositionClamp;
                                var depthSpacePoint = this.coordinateMapper.MapCameraPointToColorSpace(position);
                                jointPoints[jointType] = new Point(depthSpacePoint.X, depthSpacePoint.Y);
                                JointType parent = Helpers.GetOrigintJoint(jointType);

                                if (this.typeOfEvaluation == "right" && parent != JointType.Head && r_hip != null && r_shoulder != null && r_elbow != null && r_wrist != null && neck != null)
                                {
                                    #region Right Rula
                                    double r_hip_shoulder = Helpers.Angle(r_shoulder, r_hip);
                                    double r_shoulder_elbow = Helpers.Angle(r_shoulder, r_elbow , 90);
                                    double r_elbow_wrist = Helpers.Angle(r_elbow, r_wrist ,90);
                                    double upper_arm_angle = Helpers.Inverse(r_shoulder_elbow -180);
                                    double lower_arm_angle = Helpers.Inverse((r_elbow_wrist - 180))  + upper_arm_angle;
                                    double up_spinebase = Helpers.Angle(spine_mid, up);
                                    double neck_spine_base = Helpers.Angle(neck, spine_mid);
                                    //double trunk_angle = (Angle(up, r_hip) + Angle(r_hip, neck)) - 270;
                                    double trunk_angle = neck_spine_base - 90;
                                    double neck_head = Helpers.Angle(head, neck);
                                    double neck_angle = (neck_head - trunk_angle) - 90 - 20;

                                    #region Scores
                                     score_upper_armk = Helpers.GetRulaUpperArmScore(upper_arm_angle);
                                     score_lower_armk = Helpers.GetRulaLowerArmScore(lower_arm_angle);
                                     score_trunk = Helpers.GetRebaTrunkScore(trunk_angle);
                                     score_neck = Helpers.GetRebaNeckScore(neck_angle);
                                    #endregion


                                    //txt_kinect_info.Content += "UPPER ARM:" + upper_arm_angle.ToString("00.0") + "\n";
                                    //txt_kinect_info.Content += "UPPER ARM SCORE:" + score_upper_armk.ToString("0") + "\n";

                                    //txt_kinect_info.Content += "LOWER ARM:" + lower_arm_angle.ToString("00.0") + "\n";
                                    //txt_kinect_info.Content += "LOWER ARM SCORE:" + score_lower_armk.ToString("0") + "\n";

                                    //txt_kinect_info.Content += "Trunk Angle:" + trunk_angle.ToString("00.0") + "\n";
                                    //txt_kinect_info.Content += "Trunk Score:" + score_trunk.ToString("00.0") + "\n";

                                    //txt_kinect_info.Content += "Neck Angle:" + neck_angle.ToString("00.0") + "\n";
                                    //txt_kinect_info.Content += "Neck Score:" + score_neck.ToString("00.0") + "\n";
                                    #endregion
                                }
                                else if (this.typeOfEvaluation == "left" &&  parent != JointType.Head && l_hip != null && l_shoulder != null && l_elbow != null && l_wrist != null && neck != null)
                                {
                                    double l_hip_shoulder = Helpers.Angle(l_shoulder, l_hip);
                                    double l_shoulder_elbow = Helpers.Angle(l_shoulder, l_elbow, 90);
                                    double l_elbow_wrist = Helpers.Angle(l_elbow, l_wrist, 90);
                                    double upper_arm_angle = Helpers.Inverse(l_shoulder_elbow - 180);
                                    double lower_arm_angle = Helpers.Inverse((l_elbow_wrist - 180)) + upper_arm_angle;
                                    double up_spinebase = Helpers.Angle(spine_mid, up);
                                    double neck_spine_base = Helpers.Angle(neck, spine_mid);
                                    //double trunk_angle = (Angle(up, r_hip) + Angle(r_hip, neck)) - 270;
                                    double trunk_angle = neck_spine_base - 90;
                                    double neck_head = Helpers.Angle(head, neck);
                                    double neck_angle = (neck_head - trunk_angle) - 90 - 20;

                                    #region Scores
                                     score_upper_armk = Helpers.GetRulaUpperArmScore(upper_arm_angle);
                                     score_lower_armk = Helpers.GetRulaLowerArmScore(lower_arm_angle);
                                     score_trunk = Helpers.GetRebaTrunkScore(trunk_angle);
                                     score_neck = Helpers.GetRebaNeckScore(neck_angle);
                                    #endregion
                                    //txt_kinect_info.Content += "UPPER ARM:" + upper_arm_angle.ToString("00.0") + "\n";
                                    //txt_kinect_info.Content += "UPPER ARM SCORE:" + score_upper_armk.ToString("0") + "\n";

                                    //txt_kinect_info.Content += "LOWER ARM:" + lower_arm_angle.ToString("00.0") + "\n";
                                    //txt_kinect_info.Content += "LOWER ARM SCORE:" + score_lower_armk.ToString("0") + "\n";

                                    //txt_kinect_info.Content += "Trunk Angle:" + trunk_angle.ToString("00.0") + "\n";
                                    //txt_kinect_info.Content += "Trunk Score:" + score_trunk.ToString("00.0") + "\n";

                                    //txt_kinect_info.Content += "Neck Angle:" + neck_angle.ToString("00.0") + "\n";
                                    //txt_kinect_info.Content += "Neck Score:" + score_neck.ToString("00.0") + "\n";


                                }
                                 
                                
                                
                            }
                            this.DrawBody(joints, jointPoints, dc, drawPen);
                            //this.DrawHand(body.HandLeftState, jointPoints[JointType.HandLeft], dc);
                            //this.DrawHand(body.HandRightState, jointPoints[JointType.HandRight], dc);
                        }
                    }
                    // prevent drawing outside of our render area
                    this.drawingGroup.ClipGeometry = new RectangleGeometry(new Rect(0.0, 0.0, this.displayWidth, this.displayHeight));
                }

            
            }
        }
        private void ColorFrameReaderFrameArrived(object sender, ColorFrameArrivedEventArgs e)
       {
           using (ColorFrame colorFrame = e.FrameReference.AcquireFrame())
           {
               if (colorFrame != null)
               {
                   FrameDescription colorFrameDescription = colorFrame.FrameDescription;
                   using (KinectBuffer colorBuffer = colorFrame.LockRawImageBuffer())
                   {
                       this.bitmap.Lock();

                       // verify data and write the new color frame data to the display bitmap
                       if ((colorFrameDescription.Width == this.bitmap.PixelWidth) && (colorFrameDescription.Height == this.bitmap.PixelHeight))
                       {
                           colorFrame.CopyConvertedFrameDataToIntPtr(
                               this.bitmap.BackBuffer,
                               (uint)(colorFrameDescription.Width * colorFrameDescription.Height * 4),
                               ColorImageFormat.Bgra);

                           this.bitmap.AddDirtyRect(new Int32Rect(0, 0, this.bitmap.PixelWidth, this.bitmap.PixelHeight));
                       }

                       this.bitmap.Unlock();
                   }
               }
           }
       }
        /// <summary>
        /// Draws a body
        /// </summary>
        /// <param name="joints">joints to draw</param>
        /// <param name="jointPoints">translated positions of joints to draw</param>
        /// <param name="drawingContext">drawing context to draw to</param>
        /// <param name="drawingPen">specifies color to draw a specific body</param>
        private void DrawBody(IReadOnlyDictionary<JointType, Joint> joints, IDictionary<JointType, Point> jointPoints, DrawingContext drawingContext, Pen drawingPen)
        {

            foreach (var bone in this.bones)
            {
                this.DrawBone(joints, jointPoints, bone.Item1, bone.Item2, drawingContext, drawingPen);
            }
            Typeface t = new Typeface("Arial");
            foreach (JointType jointType in joints.Keys)
            {
                Brush drawBrush = null;
                TrackingState trackingState = joints[jointType].TrackingState;

                if (trackingState == TrackingState.Tracked)
                {
                    drawBrush = this.trackedJointBrush;
                    //FormattedText text = new FormattedText(jointType.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, t, 2, drawBrush);
                    //drawingContext.DrawText(text, jointPoints[jointType]);

                }
                else if (trackingState == TrackingState.Inferred)
                {
                    drawBrush = this.inferredJointBrush;
                }

                if (drawBrush != null)
                {
                    drawingContext.DrawEllipse(drawBrush, null, jointPoints[jointType], JointThickness, JointThickness);
                    if (this.bones.Exists(x => x.Item1.Equals(jointType)) && this.drawNames)
                    {

                        this.drawtext(drawingContext, jointType.ToString(), jointPoints[jointType]);
                    }
                }
            }
        }
        /// <summary>
        /// Draws one bone of a body (joint to joint)
        /// </summary>
        /// <param name="joints">joints to draw</param>
        /// <param name="jointPoints">translated positions of joints to draw</param>
        /// <param name="jointType0">first joint of bone to draw</param>
        /// <param name="jointType1">second joint of bone to draw</param>
        /// <param name="drawingContext">drawing context to draw to</param>
        /// /// <param name="drawingPen">specifies color to draw a specific bone</param>
        private void DrawBone(IReadOnlyDictionary<JointType, Joint> joints, IDictionary<JointType, Point> jointPoints, JointType jointType0, JointType jointType1, DrawingContext drawingContext, Pen drawingPen)
        {
            Joint joint0 = joints[jointType0];
            Joint joint1 = joints[jointType1];
            // If we can't find either of these joints, exit
            if (joint0.TrackingState == TrackingState.NotTracked ||
                joint1.TrackingState == TrackingState.NotTracked)
            {
                return;
            }
            // We assume all drawn bones are inferred unless BOTH joints are tracked
            Pen drawPen = this.inferredBonePen;
            if ((joint0.TrackingState == TrackingState.Tracked) && (joint1.TrackingState == TrackingState.Tracked))
            {
                drawPen = drawingPen;
            }

            drawingContext.DrawLine(drawPen, jointPoints[jointType0], jointPoints[jointType1]);

        }
        /// <summary>
        /// Draws a hand symbol if the hand is tracked: red circle = closed, green circle = opened; blue circle = lasso
        /// </summary>
        /// <param name="handState">state of the hand</param>
        /// <param name="handPosition">position of the hand</param>
        /// <param name="drawingContext">drawing context to draw to</param>
        private void DrawHand(HandState handState, Point handPosition, DrawingContext drawingContext)
        {
            switch (handState)
            {
                case HandState.Closed:

                    drawingContext.DrawEllipse(this.handClosedBrush, null, handPosition, HandSize, HandSize);
                    break;

                case HandState.Open:
                    drawingContext.DrawEllipse(this.handOpenBrush, null, handPosition, HandSize, HandSize);
                    break;

                case HandState.Lasso:
                    drawingContext.DrawEllipse(this.handLassoBrush, null, handPosition, HandSize, HandSize);
                    break;
            }
            this.drawtext(drawingContext, handState.ToString(), handPosition);
        }
        public void drawtext(DrawingContext _drawingContext, string _text, Point _point)
        {
            FormattedText t = new FormattedText(_text, CultureInfo.GetCultureInfo("en-us"),
     FlowDirection.LeftToRight,
     new Typeface("Verdana"),
     15,
     Brushes.White);
            _drawingContext.DrawText(t, _point);
        }
        /// <summary>
        /// Draws indicators to show which edges are clipping body data
        /// </summary>
        /// <param name="body">body to draw clipping information for</param>
        /// <param name="drawingContext">drawing context to draw to</param>
        private void DrawClippedEdges(Body body, DrawingContext drawingContext)
        {
            FrameEdges clippedEdges = body.ClippedEdges;

            if (clippedEdges.HasFlag(FrameEdges.Bottom))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(0, this.displayHeight - ClipBoundsThickness, this.displayWidth, ClipBoundsThickness));
            }

            if (clippedEdges.HasFlag(FrameEdges.Top))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(0, 0, this.displayWidth, ClipBoundsThickness));
            }

            if (clippedEdges.HasFlag(FrameEdges.Left))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(0, 0, ClipBoundsThickness, this.displayHeight));
            }

            if (clippedEdges.HasFlag(FrameEdges.Right))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(this.displayWidth - ClipBoundsThickness, 0, ClipBoundsThickness, this.displayHeight));
            }
        }



        /// <summary>
        /// Handles the event which the sensor becomes unavailable (E.g. paused, closed, unplugged).
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void Sensor_IsAvailableChanged(object sender, IsAvailableChangedEventArgs e)
        {
            // on failure, set the status text
            this.StatusText = this.kinectSensor.IsAvailable ? ProjectK.ErgoMC.Assessment.Properties.Resources.RunningStatusText : ProjectK.ErgoMC.Assessment.Properties.Resources.SensorNotAvailableStatusText;


            img_status.Source = this.kinectSensor.IsAvailable ? new BitmapImage(new Uri(@"/Images/Status_running.png", UriKind.Relative)) :
                new BitmapImage(new Uri(@"/Images/Status_notfound.png", UriKind.Relative))
                ;
        }
        /// <summary>
        /// Reader for color frames
        /// </summary>
        private bool drawNames = false;
        private readonly int _bytePerPixel = (PixelFormats.Bgr32.BitsPerPixel + 7) / 8;
        /// <summ
        /// <summary>
        /// Bitmap to display
        /// </summary>
        private WriteableBitmap colorBitmap = null;
        private void Reader_ColorFrameArrived(object sender, ColorFrameArrivedEventArgs e)
        {
            // ColorFrame is IDisposable
            using (ColorFrame colorFrame = e.FrameReference.AcquireFrame())
            {
                if (colorFrame != null)
                {
                    FrameDescription colorFrameDescription = colorFrame.FrameDescription;

                    using (KinectBuffer colorBuffer = colorFrame.LockRawImageBuffer())
                    {
                        this.colorBitmap.Lock();

                        // verify data and write the new color frame data to the display bitmap
                        if ((colorFrameDescription.Width == this.colorBitmap.PixelWidth) && (colorFrameDescription.Height == this.colorBitmap.PixelHeight))
                        {
                            colorFrame.CopyConvertedFrameDataToIntPtr(
                                this.colorBitmap.BackBuffer,
                                (uint)(colorFrameDescription.Width * colorFrameDescription.Height * 4),
                                ColorImageFormat.Bgra);

                            this.colorBitmap.AddDirtyRect(new Int32Rect(0, 0, this.colorBitmap.PixelWidth, this.colorBitmap.PixelHeight));
                        }

                        this.colorBitmap.Unlock();
                        //       colorBitmap.WritePixels(
                        //new Int32Rect(0, 0, colorFrameDescription.Width, colorFrameDescription.Height),
                        // this.colorBitmap.BackBuffer,
                        //colorFrameDescription.Width * _bytePerPixel,
                        //0);
                    }
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.bodyFrameReader != null)
            {
                // BodyFrameReader is IDisposable
                this.bodyFrameReader.Dispose();
                this.bodyFrameReader = null;

            }

            if (this.kinectSensor != null)
            {
                this.kinectSensor.Close();
                this.kinectSensor = null;
            }
            this.coordinateMapper = null;
            this.displayWidth = 0;
            this.displayHeight = 0;
            this.bodyFrameReader = null;
            this.bones = null;
            this.bodyColors = null;
            this.drawingGroup = null;
            this.imageSource = null;
            this.DataContext = null;
            this.scanType = ScanType.Arm;

            init();


            this.bodyFrameReader.FrameArrived += this.Reader_FrameArrived;
        }
        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            this.drawNames = !this.drawNames;
        }

        //rdb/checkbox
      
        
        //Evaluate
        private void btn_evaluate(object sender, RoutedEventArgs e)
        {
           
            int ctr = 0;

            foreach (IndexScore _score in this.rebaObject.getScoreList())
            {
                if (!_score.validate())
                {
                    Helpers.ToastError(Window.GetWindow(this), "Input error", _score.error_message, MessageBoxButton.OK);
                    return;
                }
                ctr++;
            }

            RebaReport _view;
            if (this._employee != null) _view = new RebaReport(this.RebaObject, this._employee , typeOfEvaluation);
            else  _view = new RebaReport(this.RebaObject, typeOfEvaluation);
            _view.Show();
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Helpers.HandleEvent(sender, e, this.rebaObject); 
        }
        private void TextBox_Clicked(object sender, RoutedEventArgs e)
        {
           
            var textbox = sender as System.Windows.Controls.TextBox;
            if (!Helpers.IsTextAllowed(textbox.Text))
            {
                textbox.Text = "0";
                textbox.MaxLength = 1;
                MessageBox.Show(Window.GetWindow(this), "Please Enter a valid number.");
            }
            if (textbox.Tag == null) return;
            String _tag = textbox.Tag.ToString();
            ChoiceSelection window = new ChoiceSelection(_tag);
            window.ShowDialog();
            switch (_tag)
            {
                case "neck_position":
                    this.rebaObject.Score_neck.SetScore(Helpers.Convert(textbox.Text));
                break;
                case "trunk_position":
                    this.rebaObject.Score_trunk.SetScore(Helpers.Convert(textbox.Text));
                    break;
                case "legs_position":
                    this.rebaObject.Score_legs.SetScore(Helpers.Convert(textbox.Text));
                    break;
                case "neck_trunk_legs_load":
                    this.rebaObject.Score_neck_trunk_legs_load.SetScore(Helpers.Convert(textbox.Text));
                    break;
                case "upper_arm":
                    this.rebaObject.Score_upper_arm.SetScore(Helpers.Convert(textbox.Text));
                    break;
                case "lower_arm":
                    this.rebaObject.Score_lower_arm.SetScore(Helpers.Convert(textbox.Text));
                    break;
                case "wrist_position":
                    this.rebaObject.Score_wrist_position.SetScore(Helpers.Convert(textbox.Text));
                    break;

                case "coupling":
                    this.rebaObject.Score_coupling.SetScore(Helpers.Convert(textbox.Text));
                    break;
                case "activity":
                    this.rebaObject.Score_activity.SetScore(Helpers.Convert(textbox.Text));
                    break;

            }
        }
        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
           
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var rdb = sender as System.Windows.Controls.Control;
            if (rdb.Tag == null) return;
            String _tag = rdb.Tag.ToString();
            string _contentText = Helpers.GetContentText(sender);
            string _group = rdb.Uid.ToString();
            switch (_group)
            {
                case "upper_arm":
                    this.rebaObject.score_upper_arm.currentAdditionalChoices.Remove(this.rebaObject.score_upper_arm.currentAdditionalChoices.Find(x => x.Content == _contentText));
                    this.rebaObject.score_upper_arm.AdditionalScore -= (Helpers.Convert(_tag));
                    break;
                case "lower_arm":
                    this.rebaObject.score_lower_arm.currentAdditionalChoices.Remove(this.rebaObject.score_lower_arm.currentAdditionalChoices.Find(x => x.Content == _contentText));
                    this.rebaObject.score_lower_arm.AdditionalScore -= (Helpers.Convert(_tag));
                    break;
                case "wrist_position":
                    this.rebaObject.score_wrist_position.currentAdditionalChoices.Remove(this.rebaObject.score_wrist_position.currentAdditionalChoices.Find(x => x.Content == _contentText));
                    this.rebaObject.score_wrist_position.AdditionalScore -= (Helpers.Convert(_tag));
                    break;
                case "wrist_twist":
                    this.rebaObject.score_coupling.currentAdditionalChoices.Remove(this.rebaObject.score_coupling.currentAdditionalChoices.Find(x => x.Content == _contentText));
                    this.rebaObject.score_coupling.AdditionalScore -= (Helpers.Convert(_tag));
                break;
                case "neck_position":
                    this.rebaObject.score_neck.currentAdditionalChoices.Remove(this.rebaObject.score_neck.currentAdditionalChoices.Find(x => x.Content == _contentText));
                    this.rebaObject.score_neck.AdditionalScore -= (Helpers.Convert(_tag));
                break;
                case "trunk_position":
                    this.rebaObject.score_trunk.currentAdditionalChoices.Remove(this.rebaObject.score_trunk.currentAdditionalChoices.Find(x => x.Content == _contentText));

                    this.rebaObject.score_trunk.AdditionalScore -= (Helpers.Convert(_tag));
                    break;
                case "legs_position":
                    this.rebaObject.score_legs.currentAdditionalChoices.Remove(this.rebaObject.score_legs.currentAdditionalChoices.Find(x => x.Content == _contentText));

                    this.rebaObject.score_legs.AdditionalScore -= (Helpers.Convert(_tag));
                    break;
                case "activity":
                    this.rebaObject.score_activity.currentAdditionalChoices.Remove(this.rebaObject.score_activity.currentAdditionalChoices.Find(x => x.Content == _contentText));
                    this.rebaObject.score_activity.AdditionalScore -= (Helpers.Convert(_tag));
                break;
                case "neck_trunk_legs_load":
                    this.rebaObject.score_neck_trunk_legs_load.currentAdditionalChoices.Remove(this.rebaObject.score_neck_trunk_legs_load.currentAdditionalChoices.Find(x => x.Content == _contentText));
                    this.rebaObject.score_neck_trunk_legs_load.AdditionalScore -= (Helpers.Convert(_tag));
                    break;
            }
        }

         private void Button_Click_1(object sender, RoutedEventArgs e)
        {
             
            this.rebaObject.Score_neck.Score = score_neck;
            this.rebaObject.Score_trunk.Score = score_trunk;
            this.rebaObject.Score_lower_arm.Score = score_lower_armk;
            this.rebaObject.Score_upper_arm.Score = score_upper_armk;
            foreach (RadioButton child in UpperArmSp.Children)
            {
                if (child.Tag.ToString() == score_upper_armk.ToString())
                {
                    child.IsChecked = true;
                }
            }
            foreach (RadioButton child in LowerArmSp.Children)
            {
                if (child.Tag.ToString() == score_lower_armk.ToString())
                {
                    child.IsChecked = true;
                }
            }
            foreach (RadioButton child in NeckSp.Children)
            {
                if (child.Tag.ToString() == score_neck.ToString())
                {
                    child.IsChecked = true;
                }
            }
            foreach (RadioButton child in TrunkSp.Children)
            {
                if (child.Tag.ToString() == score_trunk.ToString())
                {
                    child.IsChecked = true;
                }
            }
        }

        
    }


    
}
