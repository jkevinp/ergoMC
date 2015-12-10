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
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Shapes;
    /// <summary>
    /// Interaction logic for MainWindow
    /// </summary>
    public partial class Rula : Page, INotifyPropertyChanged
    {
        private RulaObject rulaObject = new RulaObject();
        public RulaObject RulaObject { get; set; }


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
        WriteableBitmap bitmap;
        public enum ScanType
        {
            UpperBody,
            LowerBody,
            Arm,
            All
        }
        private Employee _employee = null;
        public Rula(Employee _emp)
        {
            init();
            this.InitializeComponent();
            this._employee = _emp;
        }
        private ScanType scanType = ScanType.All;
        public void init()
        {
            this.kinectSensor = KinectSensor.GetDefault();
            this.coordinateMapper = this.kinectSensor.CoordinateMapper;
         
            FrameDescription frameDescription =      this.kinectSensor.DepthFrameSource.FrameDescription;
            FrameDescription colorFrameDescription = this.kinectSensor.ColorFrameSource.FrameDescription;
            this.displayWidth = colorFrameDescription.Width;
            this.displayHeight = colorFrameDescription.Height;
            this.bitmap = new WriteableBitmap(colorFrameDescription.Width, colorFrameDescription.Height, 96.0, 96.0, PixelFormats.Bgr32, null);
            this.bodyFrameReader =  this.kinectSensor.BodyFrameSource.OpenReader();
            this.colorFrameReader = this.kinectSensor.ColorFrameSource.OpenReader();

            if (this.bodyFrameReader != null)  this.bodyFrameReader.FrameArrived += this.Reader_FrameArrived;
            if (this.colorFrameReader != null) this.colorFrameReader.FrameArrived += this.ColorFrameReaderFrameArrived;
            this.bones = new List<Tuple<JointType, JointType>>();
            switch (scanType)
            {
                case ScanType.All:
                    addJointTorso();
                    addJointArm();
                    addJointLeg();
                break;
                case ScanType.Arm:
               // addJointTorso();
                addJointArm();
                break;
                case ScanType.LowerBody:
                addJointLeg();
                break;
                case ScanType.UpperBody:
                addJointArm();
                break;
                default:
                    addJointTorso();
                    addJointArm();
                    addJointLeg();
                break;
            }
            this.bodyColors = new List<Pen>();
            this.bodyColors.Add(new Pen(Brushes.Red, 3));
            this.bodyColors.Add(new Pen(Brushes.Orange, 3));
            this.bodyColors.Add(new Pen(Brushes.Green, 3));
            this.bodyColors.Add(new Pen(Brushes.Blue, 3));
            this.bodyColors.Add(new Pen(Brushes.Indigo, 3));
            this.bodyColors.Add(new Pen(Brushes.Violet, 3));
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
        public Rula()
        {
            init();
            this.InitializeComponent();    
        }
        private void addJointLeg()
        {

            // Right Leg
            this.bones.Add(new Tuple<JointType, JointType>(JointType.HipRight, JointType.KneeRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.KneeRight, JointType.AnkleRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.AnkleRight, JointType.FootRight));

            // Left Leg
            this.bones.Add(new Tuple<JointType, JointType>(JointType.HipLeft, JointType.KneeLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.KneeLeft, JointType.AnkleLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.AnkleLeft, JointType.FootLeft));
        }
        public void addJointTorso()
        {
            // Torso
            this.bones.Add(new Tuple<JointType, JointType>(JointType.Head, JointType.Neck));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.Neck, JointType.SpineShoulder));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineShoulder, JointType.SpineMid));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineMid, JointType.SpineBase));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineShoulder, JointType.ShoulderRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineShoulder, JointType.ShoulderLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineBase, JointType.HipRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineBase, JointType.HipLeft));

        }
        public void addJointArm()
        {
            // Right Arm
            //this.bones.Add(new Tuple<JointType, JointType>(JointType.ShoulderRight, JointType.ElbowRight));
            //this.bones.Add(new Tuple<JointType, JointType>(JointType.HipRight, JointType.ShoulderRight));
            //this.bones.Add(new Tuple<JointType, JointType>(JointType.ElbowRight, JointType.WristRight));


            this.bones.Add(new Tuple<JointType, JointType>(JointType.HipRight, JointType.Neck));
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

        /// <summary>
        /// INotifyPropertyChangedPropertyChanged event to allow window controls to bind to changeable data
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Gets the bitmap to display
        /// </summary>

        public ImageSource BodyImageSource
        {
            get
            {
                return this.bitmap;
            }
        }
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
                txt_kinect_info.Content = this.scanType.ToString();
                this.lb_orientations.Items.Clear();
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

                            CameraSpacePoint a = new CameraSpacePoint();
                            CameraSpacePoint b = new CameraSpacePoint();
                            CameraSpacePoint c = new CameraSpacePoint();
                            CameraSpacePoint d = new CameraSpacePoint();
                            CameraSpacePoint neck = new CameraSpacePoint();
                            CameraSpacePoint up = new CameraSpacePoint();

                            foreach (JointType jointType in joints.Keys)
                            {
                                CameraSpacePoint position = joints[jointType].Position;
                                float x = position.X;
                                switch (jointType)
                                {
                                    case JointType.ShoulderRight:
                                            b.X = position.X;
                                            b.Y = position.Y;
                                    break;
                                    case JointType.HipRight:
                                         a.X = position.X;
                                         a.Y = position.Y;
                                        //this is for trunk
                                          up.X = position.X;
                                          up.Y = position.Y + 0.1f;
                                    break;
                                    case JointType.ElbowRight:
                                        c.X = position.X;
                                        c.Y = position.Y;
                                    break;
                                    case JointType.WristRight:
                                        d.X = position.X;
                                        d.Y = position.Y;
                                    break;
                                    case JointType.Neck:
                                        neck.X = position.X;
                                        neck.Y = position.Y;
                                     break;
                                    
                                }
                                if (position.Z < 0)
                                {
                                    //position.Z = InferredZPositionClamp;
                                }
                                var depthSpacePoint = this.coordinateMapper.MapCameraPointToColorSpace(position);
                                jointPoints[jointType] = new Point(depthSpacePoint.X, depthSpacePoint.Y);
                                JointType parent = Helpers.GetOrigintJoint(jointType);
                                if (parent != JointType.Head && a != null  && b != null && c != null && d != null && neck != null)
                                {

                                    double priorAngle = Angle(a, b);
                                    double nextAngle = Angle(b, c);
                                    double thirdAngle = Angle(c, d);
                                    double upper_arm_angle = (priorAngle + nextAngle) - 360;
                                    double lower_arm_angle = ((nextAngle + thirdAngle)% 360);
                                    double trunk_angle = (Angle(up, a) + Angle(a, neck)) - 270;
                                    int score_upper_armk = 0;
                                    int score_lower_armk = 0;
                                    #region upper_arm_angle
                                    if (upper_arm_angle >= -20 && upper_arm_angle <= 20)
                                    {
                                        score_upper_armk = 1;
                                    }
                                    else if (upper_arm_angle < -20)
                                    {
                                        score_upper_armk = 2;
                                    }
                                    else if (upper_arm_angle > 20 && upper_arm_angle <= 45)
                                    {
                                        score_upper_armk = 2;
                                    }
                                    else if (upper_arm_angle > 45 && upper_arm_angle <= 90)
                                    {
                                        score_upper_armk = 3;
                                    }
                                    else if (upper_arm_angle > 90)
                                    {
                                        score_upper_armk = 4;
                                    }
#endregion
                                    #region lower_arm_angle
                                    if (lower_arm_angle >= -20 && lower_arm_angle <= 20)
                                    {
                                        score_lower_armk = 1;
                                    }
                                    else if (lower_arm_angle < -20)
                                    {
                                        score_lower_armk = 2;
                                    }
                                    else if (lower_arm_angle > 20 && lower_arm_angle <= 45)
                                    {
                                        score_lower_armk = 2;
                                    }
                                    else if (lower_arm_angle > 45 && lower_arm_angle <= 90)
                                    {
                                        score_lower_armk = 3;
                                    }
                                    else if (lower_arm_angle > 90)
                                    {
                                        score_lower_armk = 4;
                                    }
                                    #endregion


                                    txt_kinect_info.Content += "UPPER ARM:" + upper_arm_angle.ToString("00.0") + "\n";
                                    txt_kinect_info.Content += "UPPER ARM SCORE:" + score_upper_armk.ToString("0") + "\n";
                                    txt_kinect_info.Content += "LOWER ARM:" + lower_arm_angle.ToString("00.0") + "\n";
                                    txt_kinect_info.Content += "LOWER ARM SCORE:" + score_lower_armk.ToString("0") + "\n";
                                    txt_kinect_info.Content += "Trunk Angle:" + trunk_angle.ToString("00.0") + "\n";
                                
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
        public void DrawPoint(ColorSpacePoint point)
        {
            // Create an ellipse.
            Ellipse ellipse = new Ellipse
            {
                Width = 20,
                Height = 20,
                Fill = Brushes.Red
            };
            Canvas.SetLeft(ellipse, point.X - ellipse.Width / 2);
            Canvas.SetTop(ellipse, point.Y - ellipse.Height / 2);
            canvas.Children.Add(ellipse);
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
        private static double Angle(CameraSpacePoint v1, CameraSpacePoint v2, double offsetInDegrees = 0.0)
        {
            return (RadianToDegree(Math.Atan2(-v2.Y + v1.Y, -v2.X + v1.X)) + offsetInDegrees) % 360.0;
        }
        public static double RadianToDegree(double radian)
        {
            var degree = radian * (180.0 / Math.PI);
            if (degree < 0) degree = 360 + degree;
            return degree;
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
           
         
            img_status.Source = this.kinectSensor.IsAvailable ? new BitmapImage(new Uri(@"/Images/Status_running.png", UriKind.Relative)):
                new BitmapImage(new Uri(@"/Images/Status_notfound.png" , UriKind.Relative))
                ;
        }
        /// <summary>
        /// Reader for color frames
        /// </summary>
        private bool drawNames = false;       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.bodyFrameReader != null){
                this.bodyFrameReader.Dispose();
                this.bodyFrameReader = null;
            }
            if (this.kinectSensor != null){
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
        #region Forms
        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            this.drawNames = !this.drawNames;
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var rdb = sender as System.Windows.Controls.Control;
            if (rdb.Tag == null) return;
            String _tag = rdb.Tag.ToString();
            string _group = rdb.Uid.ToString();
            switch (_group)
            {

                case "upper_arm":
                    this.rulaObject.score_upper_arm.AdditionalScore += (Helpers.Convert(_tag));
                    break;
                case "lower_arm":
                    this.rulaObject.score_lower_arm.AdditionalScore += (Helpers.Convert(_tag));
                    break;
                case "wrist_position":
                    this.rulaObject.score_wrist_position.AdditionalScore += (Helpers.Convert(_tag));
                    break;
                case "wrist_twist":
                    this.rulaObject.score_wrist_twist.AdditionalScore += (Helpers.Convert(_tag));
                    break;
                case "arm_wrist_muscle":
                    this.rulaObject.score_arm_wrist_muscle.AdditionalScore += (Helpers.Convert(_tag));
                    break;
                case "arm_wrist_load":
                    this.rulaObject.score_arm_wrist_load.AdditionalScore += (Helpers.Convert(_tag));
                    break;
                case "neck_position":
                    this.rulaObject.score_neck.AdditionalScore += (Helpers.Convert(_tag));
                    break;
                case "trunk_position":
                    this.rulaObject.score_trunk.AdditionalScore += (Helpers.Convert(_tag));
                    break;
                case "legs_position":
                    this.rulaObject.score_legs.AdditionalScore += (Helpers.Convert(_tag));
                    break;
                case "neck_trunk_legs_muscle":
                    this.rulaObject.score_neck_trunk_legs_muscle.AdditionalScore += (Helpers.Convert(_tag));
                    break;
                case "neck_trunk_legs_load":
                    this.rulaObject.score_neck_trunk_legs_load.AdditionalScore += (Helpers.Convert(_tag));
                    break;
            }

        }
        private void btn_evaluate(object sender, RoutedEventArgs e)
        {
            lb_orientations.Items.Clear();
            int ctr = 0;
            
            foreach (IndexScore _score in this.rulaObject.getScoreList())
            {
                lb_orientations.Items.Add(_score.getTotal().ToString());
                if (!_score.validate())
                {

                    Helpers.ToastError(Window.GetWindow(this), "Input error", _score.error_message, MessageBoxButton.OK);
                    return;
                }
                ctr++;
            }

            EmployeeView _view;
            if (this._employee != null)
            {
                _view = new EmployeeView(this.rulaObject , this._employee);
            }
            else
            {
                _view = new EmployeeView(this.rulaObject);
            }
            _view.Show();
        }
        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

            var textbox = sender as System.Windows.Controls.TextBox;
            if(!Helpers.IsTextAllowed(textbox.Text))
            {
                textbox.Text = "0";
                textbox.MaxLength = 1;
                MessageBox.Show(Window.GetWindow(this), "Please Enter a valid number.");
            }
           
            if(textbox.Tag == null)return;
            String _tag = textbox.Tag.ToString();

            switch (_tag)
            {
                case "upper_arm":
                    this.rulaObject.score_upper_arm.SetScore(Helpers.Convert(textbox.Text));
                    break;
                case "lower_arm":
                    this.rulaObject.score_lower_arm.SetScore(Helpers.Convert(textbox.Text));
                break;
                case "wrist_position":
                this.rulaObject.score_wrist_position.SetScore(Helpers.Convert(textbox.Text));
                break;
                case "wrist_twist":
                this.rulaObject.score_wrist_twist.SetScore(Helpers.Convert(textbox.Text));
                break;
                case "arm_wrist_muscle":
                this.rulaObject.score_arm_wrist_muscle.SetScore(Helpers.Convert(textbox.Text));
                break;
                case "arm_wrist_load":
                this.rulaObject.score_arm_wrist_load.SetScore(Helpers.Convert(textbox.Text));
                break;
                case "neck_position":
                this.rulaObject.score_neck.SetScore(Helpers.Convert(textbox.Text));
                break;
                case "trunk_position":
                this.rulaObject.score_trunk.SetScore(Helpers.Convert(textbox.Text));
                break;
                case "legs_position":
                this.rulaObject.score_legs.SetScore(Helpers.Convert(textbox.Text));
                break;
                case "neck_trunk_legs_muscle":
                this.rulaObject.score_neck_trunk_legs_muscle.SetScore(Helpers.Convert(textbox.Text));
                break;
                case "neck_trunk_legs_load":
                this.rulaObject.score_neck_trunk_legs_load.SetScore(Helpers.Convert(textbox.Text));
                break;


            }
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var rdb = sender as System.Windows.Controls.Control;
            if (rdb.Tag == null) return;
            String _tag = rdb.Tag.ToString();
            
            string _group = rdb.Uid.ToString();
            switch (_group)
            {
                case "upper_arm":
                    this.rulaObject.score_upper_arm.AdditionalScore -= (Helpers.Convert(_tag));
                    break;
                case "lower_arm":
                    this.rulaObject.score_lower_arm.AdditionalScore -= (Helpers.Convert(_tag));
                    break;
                case "wrist_position":
                    this.rulaObject.score_wrist_position.AdditionalScore -= (Helpers.Convert(_tag));
                    break;
                case "wrist_twist":
                    this.rulaObject.score_wrist_twist.AdditionalScore -= (Helpers.Convert(_tag));
                    break;
                case "arm_wrist_muscle":
                    this.rulaObject.score_arm_wrist_muscle.AdditionalScore -= (Helpers.Convert(_tag));
                    break;
                case "arm_wrist_load":
                    this.rulaObject.score_arm_wrist_load.AdditionalScore -= (Helpers.Convert(_tag));
                    break;
                case "neck_position":
                    this.rulaObject.score_neck.AdditionalScore -= (Helpers.Convert(_tag));
                    break;
                case "trunk_position":
                    this.rulaObject.score_trunk.AdditionalScore -= (Helpers.Convert(_tag));
                    break;
                case "legs_position":
                    this.rulaObject.score_legs.AdditionalScore -= (Helpers.Convert(_tag));
                    break;
                case "neck_trunk_legs_muscle":
                    this.rulaObject.score_neck_trunk_legs_muscle.AdditionalScore -= (Helpers.Convert(_tag));
                    break;
                case "neck_trunk_legs_load":
                    this.rulaObject.score_neck_trunk_legs_load.AdditionalScore -= (Helpers.Convert(_tag));
                    break;
            }
        }
        private void RadioButton_MouseRightButtonUp(object sender, MouseEventArgs e)
        {
            CheckBox rdb = sender as CheckBox;
            if (rdb.Tag == null) return;
            String _tag = rdb.Tag.ToString();
            String _value = rdb.IsChecked.ToString();
            if (rdb.IsChecked.Value)
            {
                rdb.IsChecked = false;
                _tag = "0";
            }
            string _group = rdb.Uid.ToString();
            switch (_group)
            {
                case "upper_arm":
                    this.rulaObject.score_upper_arm.SetAscore(Helpers.Convert(_tag));
                    break;
                case "lower_arm":
                    this.rulaObject.score_lower_arm.SetAscore(Helpers.Convert(_tag));
                    break;
                case "wrist_position":
                    this.rulaObject.score_wrist_position.SetAscore(Helpers.Convert(_tag));
                    break;
                case "wrist_twist":
                    this.rulaObject.score_wrist_twist.SetAscore(Helpers.Convert(_tag));
                    break;
                case "arm_wrist_muscle":
                    this.rulaObject.score_arm_wrist_muscle.SetAscore(Helpers.Convert(_tag));
                    break;
                case "arm_wrist_load":
                    this.rulaObject.score_arm_wrist_load.SetAscore(Helpers.Convert(_tag));
                    break;
                case "neck_position":
                    this.rulaObject.score_neck.SetAscore(Helpers.Convert(_tag));
                    break;
                case "trunk_position":
                    this.rulaObject.score_trunk.SetAscore(Helpers.Convert(_tag));
                    break;
                case "legs_position":
                    this.rulaObject.score_legs.SetAscore(Helpers.Convert(_tag));
                    break;
                case "neck_trunk_legs_muscle":
                    this.rulaObject.score_neck_trunk_legs_muscle.SetAscore(Helpers.Convert(_tag));
                    break;
                case "neck_trunk_legs_load":
                    this.rulaObject.score_neck_trunk_legs_load.SetAscore(Helpers.Convert(_tag));
                    break;
            }
        }
#endregion

        
    }
}
