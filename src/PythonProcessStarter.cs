using System.IO;
using System.Diagnostics;

namespace SofwareEngineeringProj
{
    /// <summary>
    /// Class whos Start function will be called on a new thread.
    /// This class starts a new process which will run the Python script for face detection,
    /// and retrieve output from the process. It will then make calls to the main form (media
    /// player) to control playback.
    /// </summary>
    public class PythonProcessStarter
    {
        private FaceDetectingPlayer formObject; //Reference to main form
        private string pythonExec; //Location for pyton executable
        private string pythonScript = Directory.GetCurrentDirectory() + "\\PythonFaceDetector.py";  //Location of face detection script
        private string cascadePath = Directory.GetCurrentDirectory() + "\\haarcascade_frontalface_default.xml"; //location of cascade file for face detection
        private ProcessStartInfo pythonProcessInfo; //Object to store startup information for new process
        private Process pythonProcess;  //Process that will run the python script
        internal bool terminate = false; //Signal thread to stop retrieving output from the process
        private bool threadDone = false;//Signal that the thread is finished
        public bool terminated = false; //Signal that the process has been terminated
        internal bool ignoreDetection = true; //Determine whether to consider the output of facial detecition system

        //Constructor taking a reference to the main form
        public PythonProcessStarter(FaceDetectingPlayer form, string PythonPath)
        {
            formObject = form;
            pythonExec = PythonPath; //set path to Python executable
            SetProcessInfo();//Sets the startup information for the Face Detection python process
        }

        /// <summary>
        /// Generates startup info for the python process.
        /// Setting UseShellExecute to false allows redirection of standard input and output streams.
        /// 
        /// </summary>
        private void SetProcessInfo()
        {
            pythonProcessInfo = new ProcessStartInfo(pythonExec);
            pythonProcessInfo.CreateNoWindow = true; //Prevents process from a=opening a CMD window
            pythonProcessInfo.UseShellExecute = false;//Prevents process from being started by operating system (using cmd shell)
            pythonProcessInfo.RedirectStandardInput = true;//Redirects InputStream for process
            pythonProcessInfo.RedirectStandardOutput = true;//Redirects OutputStream for process
            pythonProcessInfo.Arguments = pythonScript + " " + cascadePath; //Arguments required by the process
        }

        /// <summary>
        /// The main logic method to be run on separate thread.
        /// This method starts the python process and continuously retrieves data written
        /// to its standard output stream. When the process outputs the necessary strings,
        /// methods controlling playback are called on the main thread.
        /// </summary>
        public void Start()
        {
            //Initialize and Start the python process using the Startup Information
            pythonProcess = new Process { StartInfo = pythonProcessInfo };
            pythonProcess.Start();

            string pyState = "initial";

            //Get the output stream of the process
            using (StreamReader pythonOut = pythonProcess.StandardOutput)
            {
                //Loop -> read output from process until signal is received to stop

                CONTINUE_READ:
                try
                {
                    while (!terminate)
                    {
                        pyState = pythonOut.ReadLine(); //Read process output
                        switch (pyState)    //switch between methods based on process output
                        {
                            case "ready":
                                break;
                            case "detected":    //Call the "Play" method to resume playback
                                if (!ignoreDetection) formObject.Play();
                                break;
                            case "not_detected"://Call the "Pause" method to pause playback
                                if (!ignoreDetection) formObject.Pause();
                                break;
                            case "terminating":
                                break;
                            default:
                                break;
                        }
                    }
                }
                catch (System.Exception)
                {   //Catch exceptions --> check if the detection process is still running
                    //If so resume reading from the process, else let the thread running this method complete
                    pythonProcess.Refresh();
                    if (!pythonProcess.HasExited) goto CONTINUE_READ;
                }
            }
            //Terminate the face detection python process and dispose of its resources
            pythonProcess.Kill();
            pythonProcess.Dispose();
            threadDone = true;  //Signal that the method is returning and the thread is completing
        }
    }
}
