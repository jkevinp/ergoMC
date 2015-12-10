﻿namespace ProjectK.ErgoMC.Assessment
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
    /// <summary>
    /// Interaction logic for MainWindow
    /// </summary>
    public partial class Reba : Page, INotifyPropertyChanged
    {
        private RebaObject rebaObject = new RebaObject();
        public RebaObject RebaObject { get; set; }

        /// <summary>
        /// Radius of drawn hand circles
        /// </summary>
        private const double HandSize = 30;
        /// <summary>
        /// Thickness of drawn joint lines
        /// </summary>
        private const double JointThickness = 3;
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
            this.displayWidth = frameDescription.Width;
            this.displayHeight = frameDescription.Height;
            this.bodyFrameReader = this.kinectSensor.BodyFrameSource.OpenReader();
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
            this.kinectSensor.Open();

            rebaObject.Score_neck.Score = 10;
            this.DataContext = this;
        }
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public Reba()
        {
            init();
            this.InitializeComponent();
        }
        private Employee _employee = null;
        public Reba(Employee _emp)
        {
            init();
            this.InitializeComponent();
            this._employee = _emp;
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
            this.bones.Add(new Tuple<JointType, JointType>(JointType.ShoulderRight, JointType.WristRight));
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
                this.lb_orientations.Items.Clear();
                if (this.drawingGroup != null && this.bodyColors != null)
                    using (DrawingContext dc = this.drawingGroup.Open())
                    {
                        // Draw a transparent background to set the render size
                        dc.DrawRectangle(Brushes.Black, null, new Rect(0.0, 0.0, this.displayWidth, this.displayHeight));

                        int penIndex = 0;
                        foreach (Body body in this.bodies)
                        {
                            Pen drawPen = this.bodyColors[penIndex++];

                            if (body.IsTracked)
                            {
                                this.DrawClippedEdges(body, dc);
                                IReadOnlyDictionary<JointType, Joint> joints = body.Joints;
                                // convert the joint points to depth (display) space
                                Dictionary<JointType, Point> jointPoints = new Dictionary<JointType, Point>();

                                //////this.lbl_body.Text = body.JointOrientations.Count.ToString();
                                //////int ctr = 0;
                                //////foreach (var x in body.JointOrientations.Keys)
                                //////{
                                //////    CameraSpacePoint position = joints[x].Position;
                                //////    CameraSpacePoint position1 = new CameraSpacePoint();

                                //////    JointType t = Helpers.GetParentJoint(x);
                                //////    position1 = joints[x].Position;

                                //////    DepthSpacePoint depthSpacePoint = this.coordinateMapper.MapCameraPointToDepthSpace(position);
                                //////    DepthSpacePoint depthSpacePoint1 = this.coordinateMapper.MapCameraPointToDepthSpace(position1);
                                //////    // Rectangle to draw.
                                //////    System.Windows.Rect rect = new System.Windows.Rect(depthSpacePoint.X, depthSpacePoint.Y, 10, 10);

                                //////    // Set rotation on drawing context.
                                //////    Vector3D j1 = new Vector3D(joints[t].Position.X, joints[t].Position.Y, joints[t].Position.Z);
                                //////    Vector3D j2 = new Vector3D(joints[x].Position.X, joints[x].Position.Y, joints[x].Position.Z);
                                //////    double AngleRElbow = Helpers.AngleBetweenTwoVectors(j1,j2);

                                //////    dc.PushTransform(new System.Windows.Media.RotateTransform(
                                //////                        AngleRElbow, body.JointOrientations[x].Orientation.Y, body.JointOrientations[x].Orientation.Z)
                                //////                    );

                                //////    // Draw our rectangle.
                                //////    dc.DrawRectangle(null, drawPen, rect);

                                //////    // Remove transformation for rotation as its no longer needed.
                                //////    dc.Pop();
                                //////    ctr++;
                                //////}



                                foreach (JointType jointType in joints.Keys)
                                {
                                    // sometimes the depth(Z) of an inferred joint may show as negative
                                    // clamp down to 0.1f to prevent coordinatemapper from returning (-Infinity, -Infinity)
                                    CameraSpacePoint position = joints[jointType].Position;
                                    if (position.Z < 0)
                                    {
                                        //position.Z = InferredZPositionClamp;
                                    }

                                    DepthSpacePoint depthSpacePoint = this.coordinateMapper.MapCameraPointToDepthSpace(position);
                                    jointPoints[jointType] = new Point(depthSpacePoint.X, depthSpacePoint.Y);
                                    JointType parent = Helpers.GetOrigintJoint(jointType);

                                    //Quaternion r = new Quaternion(body.JointOrientations[parent].Orientation.X,
                                    //      body.JointOrientations[parent].Orientation.Y,
                                    //      body.JointOrientations[parent].Orientation.Z,
                                    //      body.JointOrientations[parent].Orientation.W);

                                    //Quaternion t = new Quaternion(body.JointOrientations[jointType].Orientation.X,
                                    //    body.JointOrientations[jointType].Orientation.Y,
                                    //    body.JointOrientations[jointType].Orientation.Z,
                                    //    body.JointOrientations[jointType].Orientation.W);



                                    ////this.drawtext(dc, t.Angle.ToString(), jointPoints[jointType]);
                                    //t.Normalize();
                                    //r.Normalize();

                                    //var angle = r.Angle - t.Angle;


                                    if (parent != JointType.Head)
                                    {
                                        Vector3D parentPosition = new Vector3D(
                                                                           joints[parent].Position.X,
                                                                           joints[parent].Position.Y,
                                                                        0
                                                                           );
                                        Vector3D currentJointPosition = new Vector3D(
                                                                                        joints[jointType].Position.X,
                                                                                        joints[jointType].Position.Y,
                                                                                       0
                                                                                    );
                                        var angle = Helpers.AngleBetweenTwoVectors(currentJointPosition, parentPosition, 90);
                                        angle *= -1;
                                        this.lb_orientations.Items.Add(jointType + ":" + angle.ToString("00.0"));
                                    }




                                }

                                this.DrawBody(joints, jointPoints, dc, drawPen);
                                this.DrawHand(body.HandLeftState, jointPoints[JointType.HandLeft], dc);
                                this.DrawHand(body.HandRightState, jointPoints[JointType.HandRight], dc);
                            }
                        }

                        // prevent drawing outside of our render area
                        this.drawingGroup.ClipGeometry = new RectangleGeometry(new Rect(0.0, 0.0, this.displayWidth, this.displayHeight));
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
            // Draw the bones
            foreach (var bone in this.bones)
            {
                this.DrawBone(joints, jointPoints, bone.Item1, bone.Item2, drawingContext, drawingPen);
            }



            Typeface t = new Typeface("Arial");

            // Draw the joints
            foreach (JointType jointType in joints.Keys)
            {
                Brush drawBrush = null;
                TrackingState trackingState = joints[jointType].TrackingState;

                if (trackingState == TrackingState.Tracked)
                {
                    drawBrush = this.trackedJointBrush;
                    FormattedText text = new FormattedText(jointType.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, t, 2, drawBrush);
                    drawingContext.DrawText(text, jointPoints[jointType]);

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
            lb_orientations.Items.Clear();
            int ctr = 0;

            foreach (IndexScore _score in this.rebaObject.getScoreList())
            {
                lb_orientations.Items.Add(_score.getTotal().ToString());
                if (!_score.validate())
                {
                    Helpers.ToastError(Window.GetWindow(this), "Input error", _score.error_message, MessageBoxButton.OK);
                    return;
                }
                ctr++;
            }

            RebaReport _view;
            if (this._employee != null)
            {
                _view = new RebaReport(this.RebaObject, this._employee);
            }
            else
            {
                _view = new RebaReport(this.RebaObject);
            }
            _view.Show();



           // Helpers.ToastSuccess(Window.GetWindow(this), "Reba Final Score", "The calculated REBA score for the current employee is " + scoree.final_score + "\n" , MessageBoxButton.OK);
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var rdb = sender as System.Windows.Controls.Control;
            if (rdb.Tag == null) return;
            String _tag = rdb.Tag.ToString();
            string _group = rdb.Uid.ToString();
            switch (_group)
            {
                case "neck_position":
                    this.rebaObject.Score_neck.AdditionalScore += (Helpers.Convert(_tag));
                    break;
                case "trunk_position":
                    this.rebaObject.Score_trunk.AdditionalScore += (Helpers.Convert(_tag));
                    break;
                case "legs_position":
                    this.rebaObject.Score_legs.AdditionalScore += (Helpers.Convert(_tag));
                    break;
                case "neck_trunk_legs_load":
                    this.rebaObject.Score_neck_trunk_legs_load.AdditionalScore += (Helpers.Convert(_tag));
                    break;
                case "upper_arm":
                    this.rebaObject.Score_upper_arm.AdditionalScore += (Helpers.Convert(_tag));
                    break;
                case "lower_arm":
                    this.rebaObject.Score_lower_arm.AdditionalScore += (Helpers.Convert(_tag));
                    break;
                case "wrist_position":
                    this.rebaObject.Score_wrist_position.AdditionalScore += (Helpers.Convert(_tag));
                    break;
                   
                case "coupling":
                    this.rebaObject.Score_coupling.AdditionalScore += (Helpers.Convert(_tag));
                    break;
                case "activity":
                    this.rebaObject.Score_activity.AdditionalScore += (Helpers.Convert(_tag));
                break;

            }
            
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
            RebaWindow window = new RebaWindow(_tag);
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
        //Textbox Score
        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
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
        private void RadioButton_MouseRightButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
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
                case "neck_position":
                    this.rebaObject.Score_neck.SetAscore(Helpers.Convert(_tag));
                    break;
                case "trunk_position":
                    this.rebaObject.Score_trunk.SetAscore(Helpers.Convert(_tag));
                    break;
                case "legs_position":
                    this.rebaObject.Score_legs.SetAscore(Helpers.Convert(_tag));
                    break;
                case "neck_trunk_legs_load":
                    this.rebaObject.Score_neck_trunk_legs_load.SetAscore(Helpers.Convert(_tag));
                    break;
                case "upper_arm":
                    this.rebaObject.Score_upper_arm.SetAscore(Helpers.Convert(_tag));
                    break;
                case "lower_arm":
                    this.rebaObject.Score_lower_arm.SetAscore(Helpers.Convert(_tag));
                    break;
                case "wrist_position":
                    this.rebaObject.Score_wrist_position.SetAscore(Helpers.Convert(_tag));
                    break;

                case "coupling":
                    this.rebaObject.Score_coupling.SetAscore(Helpers.Convert(_tag));
                    break;
                case "activity":
                    this.rebaObject.Score_activity.SetAscore(Helpers.Convert(_tag));
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
                case "neck_position":
                    this.rebaObject.Score_neck.AdditionalScore -=(Helpers.Convert(_tag));
                    break;
                case "trunk_position":
                    this.rebaObject.Score_trunk.AdditionalScore -= (Helpers.Convert(_tag));
                    break;
                case "legs_position":
                    this.rebaObject.Score_legs.AdditionalScore -= (Helpers.Convert(_tag));
                    break;
                case "neck_trunk_legs_load":
                    this.rebaObject.Score_neck_trunk_legs_load.AdditionalScore -= (Helpers.Convert(_tag));
                    break;
                case "upper_arm":
                    this.rebaObject.Score_upper_arm.AdditionalScore -= (Helpers.Convert(_tag));
                    break;
                case "lower_arm":
                    this.rebaObject.Score_lower_arm.AdditionalScore -= (Helpers.Convert(_tag));
                    break;
                case "wrist_position":
                    this.rebaObject.Score_wrist_position.AdditionalScore -= (Helpers.Convert(_tag));
                    break;

                case "coupling":
                    this.rebaObject.Score_coupling.AdditionalScore -= (Helpers.Convert(_tag));
                    break;
                case "activity":
                    this.rebaObject.Score_activity.AdditionalScore -= (Helpers.Convert(_tag));
                    break;
            }
        }
    }
}
