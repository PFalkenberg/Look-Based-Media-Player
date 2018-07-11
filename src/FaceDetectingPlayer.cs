using System;
using System.Threading;
using System.Windows.Forms;
using System.IO;

//Use the libraries required for using Windows Media Player
using AxWMPLib;
using WMPLib;

namespace SofwareEngineeringProj
{
    //Main User Interface/MediaPlayer
    public partial class FaceDetectingPlayer : Form
    {
        enum MyPlayerState { Empty, Playing, Paused, Stopped }; //Enumeraton for possible states of Media Player

        private string mediaURL; //URL defining location of media to play
        private MyPlayerState mediaState = MyPlayerState.Empty; //Variable to track state of media player

        Thread pyThread;    //Thread on which PyProcessThread's Start function will run
        private PythonProcessStarter pyProcess; //Starts Python Script process and retrieves its output

        private delegate void CrossThreadOp();  //Delegate for calling methods (with no parameters) across threads
        private CrossThreadOp BtnsPlayStateOp;  //Delegate to toggle buttons when entering "Play" state
        private CrossThreadOp BtnsPauseStateOp; //Delegate to toggle buttons when entering "Plaused" state
        private CrossThreadOp BtnsStopStateOp;  //Delegate to toggle buttons when entering "Stopped" state

        //Construct the main GUI
        public FaceDetectingPlayer()
        {
            InitializeComponent();
        }

        //Set the main GUI to its initial state
        private void Form1_Load(object sender, EventArgs e)
        {
            mediaPlayer.uiMode = "none";    //Set UI mode for MediaPlayer object
            mediaPlayer.settings.autoStart = false; //de-activates autoplay feature of media player
            BtnsPlayStateOp = PlayStateButtons;
            BtnsPauseStateOp = PauseStateButtons;
            BtnsStopStateOp = StopStateButtons;

            //This is where Python 3.6 should exist
            string defaultPythonPath = Directory.GetCurrentDirectory() + @"\Python36\python.exe";
            if (File.Exists(defaultPythonPath)) //Determine if Python Executable exists
            {
                //Create the object that will encapsulate the thread that gets input from the detection process
                pyProcess = new PythonProcessStarter(this, defaultPythonPath);
                //Start the python process and the thread that will retrieve input from the process
                pyThread = new Thread(new ThreadStart(pyProcess.Start));
                pyThread.Start();
            }
            else
            {
                MessageBox.Show("Python 3.6 with OpenCV is not installed in the same directory as this executable."
                                + Environment.NewLine + "\"" + defaultPythonPath + "\"" + Environment.NewLine
                                + "Without it, this is just a basic media player.", "Python Interpreter not Found");
            }
        }

        /// <summary>
        /// Shows a OpenFileDialog for user to select a media file to play, then
        /// sets the media player's URL to the selected file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectMedia_Click(object sender, EventArgs e)
        {
            ofdFileSelectDialog.ShowDialog();//Show dialog
            tbMediaPath.Text = ofdFileSelectDialog.FileName;//Get the filename selected
            mediaURL = tbMediaPath.Text;
            mediaPlayer.URL = mediaURL;//set the media player's url
            btnPlay.Enabled = true;//enable the play button
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            pyProcess.ignoreDetection = false;  //Toggle affect of facial detection to be ignored
            Play();
        }
        /// <summary>
        /// Changes the State of the application to "Playing", starts/resumes playing of media.
        /// Called when user presses "Play" button, or a face is detected
        /// </summary>
        internal void Play()
        {
            if (mediaState != MyPlayerState.Playing)
            {
                PlayStateButtons();
                mediaState = MyPlayerState.Playing;
                mediaPlayer.Ctlcontrols.play();
            }
        }
        /// <summary>
        /// Toggles the UI buttons to be disabled or enabled for the Playing state.
        /// Play - disabled; Pause/Stop - enabled
        /// </summary>
        internal void PlayStateButtons()
        {
            if (btnPlay.InvokeRequired)
            {
                btnPlay.Invoke(BtnsPlayStateOp);
            }
            else
            {
                btnPlay.Enabled = false;
                btnPause.Enabled = true;
                btnStop.Enabled = true;
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            pyProcess.ignoreDetection = true;
            Pause();
        }
        /// <summary>
        /// Changes state of application to "Paused", pauses playback of media.
        /// Called when user presses "Pause" Button and when no faces are detected.
        /// </summary>
        internal void Pause()
        {
            if (mediaState != MyPlayerState.Paused)
            {
                PauseStateButtons();
                mediaState = MyPlayerState.Paused;
                mediaPlayer.Ctlcontrols.pause();
            }
        }
        /// <summary>
        /// Toggles the UI buttons to be disabled or enabled for the Paused state.
        /// Play - Enabled; Pause - disabled; Stop - enabled
        /// </summary>
        internal void PauseStateButtons()
        {
            if (btnPause.InvokeRequired)
            {
                btnPause.Invoke(BtnsPauseStateOp);
            }
            else
            {
                btnPlay.Enabled = true;
                btnPause.Enabled = false;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            pyProcess.ignoreDetection = true;
            Stop();
        }
        /// <summary>
        /// Only called when the user presses the stop button. Resets the media player to
        /// the beginning of the media file.
        /// </summary>
        internal void Stop()
        {
            if(mediaState != MyPlayerState.Stopped)
            {
                StopStateButtons();
                mediaState = MyPlayerState.Stopped;
                mediaPlayer.Ctlcontrols.stop();
            }
        }
        /// <summary>
        /// Toggles the UI buttons to be disabled or enabled for the Playing state.
        /// Play - Enabled; Pause/Stop - Disabled
        /// </summary>
        internal void StopStateButtons()
        {
            if (btnStop.InvokeRequired)
            {
                btnStop.Invoke(BtnsStopStateOp);
            }
            else
            {
                btnPlay.Enabled = true;
                btnPause.Enabled = false;
                btnStop.Enabled = false;
            }
        }

        /// <summary>
        /// Handler for any execptions thrown by the MediaPlayer object.
        /// Displays these errors to user in a MessageBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediaPlayer_MediaError(object sender, _WMPOCXEvents_MediaErrorEvent e)
        {
            try
            // If the Player encounters a corrupt or missing file, 
            // show the hexadecimal error code and URL.
            {
                IWMPMedia2 errSource = e.pMediaObject as IWMPMedia2;
                IWMPErrorItem errorItem = errSource.Error;
                MessageBox.Show("Error " + errorItem.errorCode.ToString("X")
                                + " in " + errSource.sourceURL);
            }
            catch (InvalidCastException)
            // In case pMediaObject is not an IWMPMedia item.
            {
                MessageBox.Show("Error.");
            }
        }

        /// <summary>
        /// Sendss signal to thread interacting with python process to stop reading output
        /// and terminate the process
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(pyProcess != null) pyProcess.terminate = true;
        }
    }
}
