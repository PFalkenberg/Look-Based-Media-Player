import sys
import threading#For future development when process allows input commands
import cv2

#boolean flags for future devepment (ex. sending a quit/pause command to this process)
running = True
detecting = True

#Function returns "detected" if at least one face is detected, otherwise returns "not_detected"
def FaceState(numFaces):
    if(numFaces < 1):
        state = 'not_detected'
    else:
        state = 'detected'
    return state;

#Function for future development -> to be called if "pause-detection" command is received
def PauseDetection():
    detecting = False
    return;

#Function for future development -> to be called if "resume-detection" command is received
def ResumeDetection():
    detecting = True
    return;

#Function for future development -> to be called if "terminate" command is received
def Close():
    running = False
    return;

#Class for Future development -> runs as a separate thread to retreive input commands
#   in future development this thread will receive commands and call appropriate function from above
class InputThread(threading.Thread):
    #object constructor
    def __init__(self, threadId, threadName):
        self.threadId = threadId
        self.name = threadName
    #method to be run as thread
    def run(self):
        while running:
            uIn = input()#get input command -> will be expecting a string
        #call method matching the command received
        if uIn == 'close':
            Close()
        elif uIn == 'resume':
            ResumeDetection()
        elif uIn == 'pause':
            PauseDetection()
        return;

cascadePath = str(sys.argv[1])#The first argument to this process is the file path to the cascade file for face detection
face_cascade = cv2.CascadeClassifier(cascadePath)#instantiate the classifier object that will detect faces
cap = cv2.VideoCapture(0)#instantiate the object that will start retrieving input from the machine's webcam

#get initial detection state
ret, frame = cap.read()
gray = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)
faces = face_cascade.detectMultiScale(gray, 1.3, 5)
#Draws Rectangle around face
for (x,y,w,h) in faces:
        cv2.rectangle(frame,(x,y),(x+w,y+h),(255,0,0),2)
cv2.imshow('DEMO', frame)#shows the camera feed in a window -> used for demo/debugging
cv2.waitKey(1)#sets the amount of time the frame will be shown
previous_state = FaceState(len(faces))#sets the initial state of detection (detected/not_detected)
sys.stdout.write(previous_state + "\n")#writes the state to the standard output (which is redirected)
sys.stdout.flush()#flushes the standard output to the main C# process (where it has been redirected)

#Repeats the process above infinitely, while the process is running
#In future development, input will be used to control this loop
while running:
    if detecting:
        ret, frame = cap.read()
        gray = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)
        faces = face_cascade.detectMultiScale(gray, 1.3, 5)
        for (x,y,w,h) in faces:
            cv2.rectangle(frame,(x,y),(x+w,y+h),(255,0,0),2)
        cv2.imshow('DEMO', frame)
        cv2.waitKey(1)
        sys.stdout.write(FaceState(len(faces))+"\n")
        sys.stdout.flush()

#releases the Capture and Classification objects' resources and destroys the window being shown
cv2.release()
cv2.destroyAllWindows()



#new_state = FaceState(len(faces))
        #if(new_state != previous_state):
            #sys.stdout.write(new_state+"\n")
            #sys.stdout.flush()
            #previous_state = new_state
        #else:
            #sys.stdout.write("nochange\n");
            #sys.stdout.flush();