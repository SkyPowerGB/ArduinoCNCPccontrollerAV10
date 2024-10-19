
#include <math.h>
#include <AccelStepper.h>

/*--------------------------------------------------------
priprema prvo definiranje svih pinova kako bi se lakse promjenilo 
u slucaju potrebe . */
//CONSTANTS--------------------------------------------

//debug active
bool debug = false;

//stepperi
#define motorInterfaceType 1
const byte xStepPin = 2, yStepPin = 3, zStepPin = 4;

//STEP DIR
const byte xDirPin = 5, yDirPin = 6, zDirPin = 7;

const int X_RIGHT = 1, Y_FOWARD = 1, Z_UP = 1;

//STEP ENABLE
const byte stepEnablePin = 8;


//ENDSTOPS
const byte endStpPin = 12;

//Z CAL SENSOR
/*
#define zCalPin A3
*/

//STOP BUTTON
const byte stpBtnPin = 11;

//SPINDLE    (11->LEFT Z ENDST)

const byte spindleSpeedOut = 10;

//------NUM OF STEPS PER 1mm---------------------------------------------------------------------------------------------------------------------

double xAxisStepMM = 25;
double yAxisStepMM = 25;
double zAxisStepMM = 100;

//-----postavljanje osnovnih vrijednosti-----------------------------------------------------------------

double maxStepperSpeed = 120;  //step/s


double maxStepperAcc = 500;  //step/s^2

byte spindleDefault = 255;  // 255 -> 20 000 rpm
int spindleMaxRPM = 20000;

//----MACHINE POSITION-----------------------------------------
double machineAbsPosX = 0;
double machineAbsPosY = 0;
double machineAbsPosZ = 0;

//setppers
AccelStepper stepperX;
AccelStepper stepperY;
AccelStepper stepperZ;

//  USB COMMUNICATION
//  RECIVE

#define startUsbCommunication "$START"

#define beginHomeSequence "$HOME"
#define beginZcalSequence "$ZCAL"
#define beginCenterSequence "$CENTER"
#define usbCommEnd "$ENDCONN"
#define SETORIGIN "$ORIGIN"
#define disableES "$DES"
#define enableES "$EES"
#define disableStep "$DST"
#define getPositionData "$?"
#define getESstatus "$E?"
#define repeatMsg "$RC"

// REPPLY

#define usbCommBeginReply "$CONNECTED#"
#define gcodeStreamSendNext "$NEXT#"
#define stopGcodeStream "$STOP#"
#define sequenceDone "$DONE#"
#define okA "$OK#"
#define yesA "$Y#"
#define noA "$N#"
#define repeatLast "$R#"
#define errorMsg "$ERR#"


String lastRepply;


// HOMING
#define zAxisHome "G91 G0 Z40"
#define zAxisClear "G0 Z-4"

#define xAxisHome "G0 X-150"
#define xAxisClear "G0 X8"

#define yAxisHome "G0 Y150"
#define yAxisClear "G0 Y-8"

//cnetering
#define xyCenterLine "G91 G0 X73 Y-43"



void setup() {
  stepperX = AccelStepper(motorInterfaceType, xStepPin, xDirPin);
  stepperY = AccelStepper(motorInterfaceType, yStepPin, yDirPin);
  stepperZ = AccelStepper(motorInterfaceType, zStepPin, zDirPin);
  stepperX.setMaxSpeed(maxStepperSpeed);
  stepperY.setMaxSpeed(maxStepperSpeed);
  stepperZ.setMaxSpeed(maxStepperSpeed);

  stepperX.setAcceleration(maxStepperAcc);
  stepperY.setAcceleration(maxStepperAcc);
  stepperZ.setAcceleration(maxStepperAcc);

  pinMode(stpBtnPin, INPUT_PULLUP);
  pinMode(endStpPin, INPUT);


  pinMode(stepEnablePin, OUTPUT);
  disableSteppers();


  Serial.begin(9600);
  calcStuff();
}

void loop() {
  beginUsb();
}

//START USB COMMUNICATION
void beginUsb() {
  String line = readUsbLine();
  doUSBcommands(line);
}

//usb communication execution
bool usbCommRunning = false;
void usbRun() {

  usbCommRunning = true;

  while (usbCommRunning) {
    String line = readUsbLine();


    doUSBcommands(line);
  }
}

String usbLine = "";
bool running = false;



bool isGcode(String line) {
  if (line[1] == '<' && line[line.length() - 2] == '>') {
    return true;
  }
  return false;
}

String stripGcode(String line) {
  return line.substring(2, line.length() - 2);
}



// homing and calibration commands-------------------------------------------------------------------------
bool ignoreStopExecution = false;
void home() {

  //ZAXIS
  enableEndstops();
  running = true;

  String line = "G0 Z15 F20";
  while (running) {
    checkStopBtn();
    if (checkEndstops()) {
      running = false;
      break;
    }
    delay(1000);
    readCommandLine(zAxisHome);
  }
  checkStopBtn();

  disableEndstops();
  readCommandLine(zAxisClear);
  enableEndstops();


  //XAXIS
  running = true;

  line = "G91 G0 X-50 F200";
  while (running) {
    checkStopBtn();
    if (checkEndstops()) {
      running = false;
      break;
    }
    delay(100);
    readCommandLine(xAxisHome);
  }
  disableEndstops();
  readCommandLine(xAxisClear);
  enableEndstops();

  delay(10);



  //YAXIS
  running = true;

  line = "G0 Y50 F200";
  while (running) {
    checkStopBtn();
    if (checkEndstops()) {
      running = false;
      break;
    }
    delay(100);
    readCommandLine(yAxisHome);
  }
  disableEndstops();
  readCommandLine(yAxisClear);
  enableEndstops();

  machineAbsPosX = 0;
  machineAbsPosY = 0;
  machineAbsPosZ = 0;
}

bool zcal = false;
void zCal() {
  SendRepply(okA);
  zcal = true;
  enableEndstops();
  running = true;

  String line = "G0 Z-5 F20";
  while (running) {
    checkStopBtn();
    if (checkEndstops()) {
      running = false;
      break;
    }
    delay(1000);
    readCommandLine(line);
  }
  disableEndstops();
  readCommandLine("G0 Z5 F20");
  enableEndstops();


  setOrigin();
  machineAbsPosZ = 24.5;
  zcal = false;
}

void center() {
  SendRepply(okA);
  home();
  readCommandLine(xyCenterLine);
  SendRepply(sequenceDone);
}



void disableEndstops() {

  ignoreStopExecution = true;
}
void enableEndstops() {

  ignoreStopExecution = false;
}



//*******************************************************************************************%*************************************************
// read usb line
String readUsbLine() {
  if (Serial.available() > 0) {
    String line = Serial.readStringUntil('\n');
    line.trim();
    return line;
  }
  return "";
}

bool doUSBcommands(String cmd) {
  // Return false if the command is empty or consists only of whitespace
  if (cmd.length() == 0) {
    return false;
  }

  if (cmd == startUsbCommunication) {
    SendRepply(usbCommBeginReply);
    if (!usbCommRunning) {
      usbRun();
    }
    return true;
  }

  if (usbCommRunning || running) {
    if (cmd == usbCommEnd) {
      usbCommRunning = false;
      return true;
    }  
    if (cmd == beginHomeSequence) {
      SendRepply(okA);
      home();
      SendRepply(sequenceDone);
      return true;
    }
    if (cmd == beginZcalSequence) {
      SendRepply(okA);
      zCal();

      return true;
    }
    if (cmd == disableES) {
      SendRepply(okA);
      disableEndstops();
      SendRepply(sequenceDone);
      return true;
    }
    if (cmd == enableES) {
      SendRepply(okA);
      enableEndstops();
      SendRepply(sequenceDone);
      return true;
    }
    if (cmd == getESstatus) {
      if (!ignoreStopExecution) {
        SendRepply(yesA);
        return true;
      } else {
        SendRepply(noA);
        return true;
      }
    }
    if (cmd == repeatMsg) {
      SendRepply(lastRepply);
      return true;
    }
    if (cmd == SETORIGIN) {
      setOrigin();
      printStatus();
      return true;
    }
    if (cmd == getPositionData) {
      printStatus();
      return true;
    }
    if (cmd == beginCenterSequence) {
      center();
      return true;
    }
    if (cmd == disableStep) {
      disableSteppers();
      return true;
    
    }
    if(isGcode(cmd)){
         readCommandLine( stripGcode(cmd));
         SendRepply(gcodeStreamSendNext);

    }
    if(cmd!=""){
    readCommandLine(cmd);
    SendRepply(gcodeStreamSendNext);
    }
  }
  return false;
  
}

//send
void SendRepply(String repply) {
  lastRepply = repply;
  Serial.println(repply);
}



// --------Gcode reader---------------------------------------------------------------------------------------------------------

/*
 parameter
 X 1
 Y 2
 Z 3
 I 4
 J 5
 K 6
 F 7
 P 8
 D 9
 H 10
 S 11
 L 12
*/

// -----------------------Gocde izvrsitelj--------------------------------------------------------------------------------------
bool readPC = false;
bool ignore = false;
String output = "";

// G-code status variables
bool spindleOn = false;
bool spindleDirClock = true;
bool unitModeMM = true;
bool distanceModeAbs = false;
bool toolLengthOffset = false;
bool cutterCompensation = false;
bool exactStop = false;
bool feedRateModeUnitPmin = true;
byte motionMode = 0;  // g0 1 / g1 2 / g2 3 / g3 4 / g38.2-38.5 5-8 / g80 9
bool xyPlane = true;
bool xzPlane = false;
bool yzPlane = false;

// G-code parameter status variables
float feedRate = 0;
float Ivalue = 0;
float Kvalue = 0;
float Jvalue = 0;
float Rvalue = 0;
float Svalue = 0;
float Xvalue = 0;
float Yvalue = 0;
float Zvalue = 0;

//speed
float speedVar;


//stuff thats recaclucated
bool calc = false;
float xAxisResoultion;
float yAxisResoultion;
float zAxisResoultion;

void calcStuff() {
  if (!calc) {
    xAxisResoultion = 1 / xAxisStepMM;
    yAxisResoultion = 1 / yAxisStepMM;
    zAxisResoultion = 1 / zAxisStepMM;
    calc = true;
  }
}
//i za debug
int i = 1;
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Process G-code line
void readCommandLine(String gCodeLine) {

  checkStopBtn();
  Serial.println(gCodeLine);
  resetPosVars();
  calcStuff();

  for (unsigned int i = 0; i < gCodeLine.length(); i++) {
    if (gCodeLine[i] == ')') {
      ignore = false;
    } else if (gCodeLine[i] == '(') {
      ignore = true;
    }

    if (!ignore) {
      if (validChar(gCodeLine[i])) {
        readPC = true;
        output += gCodeLine[i];
      } else if (readPC) {
        gcodeSegmentReciver(output);
        readPC = false;
        output = "";
      }
    }
  }

  if (readPC && !(output == "")) {
    gcodeSegmentReciver(output);
    output = "";
    readPC = false;
  }

  if (!ignore) {
    executeCommand();
  }

  i++;
}

// Handle received G-code segment
void gcodeSegmentReciver(String code) {
  char c = code[0];
  code.remove(0, 1);
  double num = code.toDouble();

  switch (c) {
    case 'G': gCom(num); break;
    case 'M': mCom(num); break;
    case 'F': feedRate = num; break;
    case 'I': Ivalue = num; break;
    case 'J': Jvalue = num; break;
    case 'K': Kvalue = num; break;
    case 'R': Rvalue = num; break;
    case 'S': Svalue = num; break;
    case 'X': Xvalue = num; break;
    case 'Y': Yvalue = num; break;
    case 'Z': Zvalue = num; break;
  }
}

// Reset nonModal variables
void resetPosVars() {
  if (!distanceModeAbs) {
    Xvalue = 0;
    Yvalue = 0;
    Zvalue = 0;

  } else {
    Xvalue = machineAbsPosX;
    Yvalue = machineAbsPosY;
    Zvalue = machineAbsPosZ;
  }
  Ivalue = 0;
  Kvalue = 0;
  Jvalue = 0;
  Rvalue = 0;
}
// set origin
void setOrigin() {
  machineAbsPosX = 0;
  machineAbsPosY = 0;
  machineAbsPosZ = 0;
}

// Valid non num Char
/*
bool validCharNN(char c){
   switch (c) {

    case 'G':
    case 'M':
    case 'X':
    case 'Y':
    case 'Z':
    case 'I':
    case 'J':
    case 'K':
    case 'F':
    case 'P':
    case 'D':
    case 'H':
    case 'S':
    case 'T':
    case 'L': return true;
    default: return false;

}*/

// Valid character check
bool validChar(char c) {
  switch (c) {
    case '0':
    case '1':
    case '2':
    case '3':
    case '4':
    case '5':
    case '6':
    case '7':
    case '8':
    case '9':
    case '.':
    case '-':
    case 'G':
    case 'M':
    case 'X':
    case 'Y':
    case 'Z':
    case 'I':
    case 'J':
    case 'K':
    case 'F':
    case 'P':
    case 'D':
    case 'H':
    case 'S':
    case 'T':
    case 'L': return true;
    default: return false;
  }
}

//maknul 0,0,0 u g90 i dodano g54 set origin
// Handle G commands
void gCom(double num) {
  switch ((int)num) {
    case 0: motionMode = 1; break;
    case 1: motionMode = 2; break;
    case 2: motionMode = 3; break;
    case 3: motionMode = 4; break;

    case 80: motionMode = 9; break;
    case 93: feedRateModeUnitPmin = false; break;
    case 94: feedRateModeUnitPmin = true; break;
    case 20: unitModeMM = false; break;
    case 21: unitModeMM = true; break;
    case 90:
      distanceModeAbs = true;

      break;
    case 91: distanceModeAbs = false; break;
    case 54: setOrigin(); break;
    case 17:
      xyPlane = true;
      xzPlane = false;
      yzPlane = false;
      break;
    case 18:
      xyPlane = false;
      xzPlane = true;
      yzPlane = false;
      break;
    case 19:
      xyPlane = false;
      xzPlane = false;
      yzPlane = true;
      break;
  }
}

// Handle M commands
void mCom(double num) {
  switch (int(num)) {
    case 0:
    case 1: break;
    case 2:
    case 30:
      spindleOn = false;


      SendRepply(sequenceDone);

      running = false;
     



      break;
    case 3:
      spindleOn = true;
      spindleDirClock = true;
      break;
    case 4: spindleDirClock = false; break;
    case 5: spindleOn = false; break;
  }
}


// Execute command based on current state
void executeCommand() {
  checkStopBtn();


  if (spindleOn) {
    if (spindleDirClock) {
      runSpindle(calcSpindleSpeed(Svalue));
    } else {
      runSpindle(calcSpindleSpeed(Svalue));
    }
  } else {
    runSpindle(0);
  }
  switch (motionMode) {
    case 1: executeLinearMax(); break;
    case 2: executeLinear(); break;
    case 3: executeCircular(true); break;
    case 4: executeCircular(false); break;
  }

  
}

//-------Circualr motion helpers---------------------

float outputA;
float outputB;
bool doFullCircle = false;
void followCircle(float startA, float startB, float centerA, float centerB, float targetA, float targetB, float increment, float threshold, bool dirClock) {

  float ctrA = centerA / increment;
  float ctrB = centerB / increment;

  float ta = targetA / increment;
  float tb = targetB / increment;

  float sa = startA / increment;
  float sb = startB / increment;


  ctrA = floor(ctrA) * increment;
  ctrB = floor(ctrB) * increment;


  ta = floor(ta) * increment;
  tb = floor(tb) * increment;

  sa = floor(sa) * increment;
  sb = floor(sb) * increment;

  if (sa == ta && sb == tb) {
    doFullCircle = true;
  } else {
    doFullCircle = false;
  }


  debugLn("follow cirlce: " + String(sa) + "," + String(sb) + " center: " + String(ctrA) + "," + String(ctrB) + " target: " + String(ta) + "," + String(tb));



  float pstarta = sa;
  float pstartb = sb;
  float radious = getTwoPointDistance(ctrA, ctrB, ta, tb);

  int checker = (4 * radious) / increment;


  if (radious <= increment) {
    debugLn("too small circle!");
    followCircleOutput(targetA, targetB);
    return;
  }

  bool runCircle = true;
  while (runCircle) {

    if (getTwoPointDistance(pstarta, pstartb, ta, tb) < threshold && !doFullCircle) {
      debugLn("treshold reached!");
      runCircle = false;
      break;
    }

    if (checker == 0) { break; }

    checker--;
    calcNextPoint(pstarta, pstartb, radious, ctrA, ctrB, increment, dirClock);
    pstarta = outputA;
    pstartb = outputB;

    followCircleOutput(pstarta, pstartb);
  }

  followCircleOutput(targetA, targetB);
}

void findCirclePoint(float axisA, float centerAxisA, float centerAxisB, float radius, float increment, bool right, bool up) {
  outputA = axisA;
  outputB = centerAxisB;
  if (right) {
    outputA += increment;
  } else {
    outputA -= increment;
  }

  while (getTwoPointDistance(outputA, outputB, centerAxisA, centerAxisB) < radius) {
    if (up) {
      outputB += increment;
    } else {
      outputB -= increment;
    }
  }

  // DECREASE IF ITS MORE THAN RADIOUS OUTSIDE CIRCLE
  if ((getTwoPointDistance(outputA, outputB, centerAxisA, centerAxisB) - radius) > radius) {
    if (up) {
      outputB -= increment;
    } else {
      outputB += increment;
    }
  }
}

void calcNextPoint(float startA, float startB, float radius, float centerA, float centerB, float increment, bool dirClock) {
  int pointPos = getPointPosOnCircle(startA, startB, centerA, centerB);
  debugLn("point pos:" + pointPos);
  // 1-> POINT 1/4     4->POINT 4/4
  //  5 0DEG , 7 180   6 90 AND 8 270
  switch (pointPos) {
    case 1:
    case 2:
      if (dirClock) {
        findCirclePoint(startA, centerA, centerB, radius, increment, true, true);
      } else {
        findCirclePoint(startA, centerA, centerB, radius, increment, false, true);
      }
      break;
    case 3:
    case 4:
      if (dirClock) {
        findCirclePoint(startA, centerA, centerB, radius, increment, false, false);
      } else {
        findCirclePoint(startA, centerA, centerB, radius, increment, true, false);
      }
      break;
    case 5:
      if (dirClock) {
        findCirclePoint(startA, centerA, centerB, radius, increment, false, false);
      } else {
        findCirclePoint(startA, centerA, centerB, radius, increment, false, true);
      }
      break;
    case 6:
      if (dirClock) {
        findCirclePoint(startA, centerA, centerB, radius, increment, true, true);
      } else {
        findCirclePoint(startA, centerA, centerB, radius, increment, false, true);
      }
      break;
    case 7:
      if (dirClock) {
        findCirclePoint(startA, centerA, centerB, radius, increment, true, true);
      } else {
        findCirclePoint(startA, centerA, centerB, radius, increment, true, false);
      }
      break;
    case 8:
      if (dirClock) {
        findCirclePoint(startA, centerA, centerB, radius, increment, false, false);
      } else {
        findCirclePoint(startA, centerA, centerB, radius, increment, true, false);
      }
      break;
  }
}
int getPointPosOnCircle(float pointAaxis, float pointBaxis, float circleAaxis, float circleBaxis) {

  //
  if (pointBaxis == circleBaxis && pointAaxis > circleAaxis) {
    //POINT ON CNTER AXIS , RIGHT (0DEG)
    return 5;
  } else if (pointBaxis == circleBaxis && pointAaxis < circleAaxis) {
    //POINT ON CNTER AXIS , LEFT (180DEG)

    return 7;
  } else if (pointAaxis == circleAaxis && pointBaxis > circleBaxis) {
    //POINT ON CNTER AXIS , ABOVE (90DEG)

    return 6;
  } else if (pointAaxis == circleAaxis && pointBaxis < circleBaxis) {
    //POINT ON CNTER AXIS , BELOW (270DEG)

    return 8;
  } else if (pointAaxis > circleAaxis && pointBaxis > circleBaxis) {

    // POINT IN 1/4

    return 1;
  } else if (pointAaxis < circleAaxis && pointBaxis > circleBaxis) {

    // POINT IN 2/4

    return 2;
  } else if (pointAaxis < circleAaxis && pointBaxis < circleBaxis) {
    // POINT IN 3/4

    return 3;
  } else if (pointAaxis > circleAaxis && pointBaxis < circleBaxis) {
    // POINT IN 4/4
    return 4;
  } else {
    return 0;
  }
}

//--------Execution----------------------------------------------------------------------------------------------------------------------------------------------
void executeLinear() {

  linearMove(Xvalue, Yvalue, Zvalue);
}

void executeLinearMax() {
  moveAllAxis(Xvalue, Yvalue, Zvalue, maxStepperSpeed);
}
//circle precision setup
float treshHold = 0.1;
float increment = 0.08;
void executeCircular(bool dirClock) {

  float sx = 0;
  float sy = 0;
  float sz = 0;

  float ci = 0;
  float cj = 0;
  float ck = 0;

  ci = Ivalue;
  cj = Jvalue;
  ck = Kvalue;

  float tx = Xvalue;
  float ty = Yvalue;
  float tz = Zvalue;

  if (distanceModeAbs) {
    sx = machineAbsPosX;
    sy = machineAbsPosY;
    sz = machineAbsPosZ;
  }
  ci = sx + ci;
  cj = sy + cj;
  ck = sz + ck;




  if (xyPlane) {

    followCircle(sx, sy, ci, cj, tx, ty, increment, treshHold, dirClock);
  } else if (yzPlane) {

    followCircle(sy, sz, cj, ck, ty, tz, increment, treshHold, dirClock);
  } else if (xzPlane) {

    followCircle(sx, sz, ci, ck, tx, tz, increment, treshHold, dirClock);
  }
}

void followCircleOutput(float a, float b) {


  if (xyPlane) {
    linearMove(a, b, 0);
  } else if (yzPlane) {
    linearMove(0, a, b);
  } else if (xzPlane) {
    linearMove(a, 0, b);
  }
}

void linearMove(float x, float y, float z) {

  debugLn("move to: " + String(x) + " : " + String(y) + " : " + String(z));
  speedVar = feedRate;
  moveAllAxis(x, y, z, speedVar);
}
float calcMove(float absPos, float point) {
  float diff = abs(absPos - point);
  if (point < absPos) {
    return -diff;
  } else {
    return diff;
  }
}
// prep data--------------------------------------------------------------------------------------------
void moveAllAxis(float x, float y, float z, float spd) {
  enableSteppers();
  checkEndstops();

  checkStopBtn();

  float xt = 0, yt = 0, zt = 0;
  int xSteps, ySteps, zSteps;
  float xSpd, ySpd, zSpd;
  float xAcc, yAcc, zAcc;


  bool xDir = true, yDir = true, zDir = true;
  xSpd = calculateSpeedFrMMS(xAxisStepMM, spd);
  ySpd = calculateSpeedFrMMS(yAxisStepMM, spd);
  zSpd = calculateSpeedFrMMS(zAxisStepMM, spd);


  if (distanceModeAbs) {

    xt = calcMove(machineAbsPosX, x);
    yt = calcMove(machineAbsPosY, y);
    zt = calcMove(machineAbsPosZ, z);
  } else {
    xt = x;
    yt = y;
    zt = z;
  }

  if (unitModeMM) {
    xSteps = floor((xt * xAxisStepMM));
    ySteps = floor((yt * yAxisStepMM));
    zSteps = floor((zt * zAxisStepMM));
  } else {
    xSteps = floor(xt * (xAxisStepMM * 25.4));
    ySteps = floor(yt * (yAxisStepMM * 25.4));
    zSteps = floor(zt * (zAxisStepMM * 25.4));
  }


  if (xSteps < 0) {
    xSteps = xSteps * -1;
    xDir = false;
  }
  if (ySteps < 0) {
    ySteps = ySteps * -1;
    yDir = false;
  }
  if (zSteps < 0) {
    zSteps = zSteps * -1;

    zDir = false;
  }


  if (xDir) {
    machineAbsPosX += (xAxisResoultion * xSteps);
  } else {
    machineAbsPosX -= (xAxisResoultion * xSteps);
  }
  if (yDir) {
    machineAbsPosY += (yAxisResoultion * ySteps);
  } else {
    machineAbsPosY -= (yAxisResoultion * ySteps);
  }
  if (zDir) {
    machineAbsPosZ += (zAxisResoultion * zSteps);
  } else {
    machineAbsPosZ -= (zAxisResoultion * zSteps);
  }
  int primarySteps = max(xSteps, max(ySteps, zSteps));


  if (primarySteps == 0) { return; }

  float xRelSpd = (float)xSteps / primarySteps * xSpd;
  float yRelSpd = (float)ySteps / primarySteps * ySpd;
  float zRelSpd = (float)zSteps / primarySteps * zSpd;



  setMotorParameters(stepperX, xRelSpd, xAcc, xSteps, xDir);
  setMotorParameters(stepperY, yRelSpd, yAcc, ySteps, yDir);
  setMotorParameters(stepperZ, zRelSpd, zAcc, zSteps, zDir);

  moveMotors();
}
//helpers
void setMotorParameters(AccelStepper &stepper, float speed, float acceleration, int steps, bool direction) {
  stepper.setMaxSpeed(speed);
  stepper.setAcceleration(acceleration);
  long targetPosition = stepper.currentPosition() + (steps * (direction ? 1 : -1));
  stepper.moveTo(targetPosition);
}
//
float getTwoPointDistance(float x1, float y1) {
  return getTwoPointDistance(x1, y1, 0, 0);
}
float getTwoPointDistance(float x1, float y1, float x2, float y2) {
  return sqrt(pow(x2 - x1, 2) + pow(y2 - y1, 2));
}

//prep data helpers
// calculate feedrate speed;
float calculateSpeedFrMMS(float resolution, float feedRateIn) {
  if (feedRateIn <= 0) {
    feedRateIn = maxStepperSpeed;
  }
  float speed = ((feedRateIn / 60) * resolution);

  if (speed <= 0 || speed >= maxStepperSpeed) {

    if (speed > maxStepperSpeed) {
      return maxStepperSpeed;
    }
    if (speed <= 0) {
      return 1;
    }
  }
  return speed;
}

//---------MOVING STUFF---------------------------------------------------------------------
byte spindleCurrent = 0;
byte spindleDelay = 100;
void runSpindle(byte spdPWM) {
  Serial.println(spdPWM);


  if (spindleCurrent == spdPWM) {
    return;
  }
  if (spdPWM == 0) {
    spindleOFF();
  } else {
    spindleON(spdPWM);
  }
}
void spindleOFF() {
  Svalue = 0;
  while (spindleCurrent > 0) {
    spindleCurrent--;
    analogWrite(spindleSpeedOut, spindleCurrent);
    delay(spindleDelay);
  }
}
void spindleON(byte &PWM) {
  if (PWM < spindleCurrent) {
    while (spindleCurrent > PWM) {
      spindleCurrent--;

      analogWrite(spindleSpeedOut, spindleCurrent);
      delay(spindleDelay);
    }
  }
  while (spindleCurrent < PWM) {
    spindleCurrent++;
    checkStopBtn();
    analogWrite(spindleSpeedOut, spindleCurrent);
    delay(spindleDelay);
  }
}
byte calcSpindleSpeed(float rpm) {
  return (rpm / spindleMaxRPM) * 255;
}
void moveMotors() {


  while (stepperX.distanceToGo() != 0 || stepperY.distanceToGo() != 0 || stepperZ.distanceToGo() != 0) {
    checkStopBtn();

    if (checkStopBtn() || checkEndstops()) {
      disableSteppers();
      return;
    }
    enableSteppers();
    int xdist = stepperX.distanceToGo();
    int ydist = stepperY.distanceToGo();
    int zdist = stepperZ.distanceToGo();

    stepperX.run();
    if (checkStopBtn() || checkEndstops()) {
      disableSteppers();



      return;
    }
    stepperY.run();
    if (checkStopBtn() || checkEndstops()) {
      disableSteppers();


      return;
    }
    stepperZ.run();
    if (checkStopBtn() || checkEndstops()) {
      disableSteppers();



      return;
    }
  }
}

void stopExecution() {

  stopMotors();
}

void stopExecutionSB() {

  Serial.println("$STOP#");
  stopMotors();
}

void stopMotors() {

  stepperX.stop();
  stepperY.stop();
  stepperZ.stop();
  stepperX.setAcceleration(1000);
  stepperY.setAcceleration(1000);
  stepperZ.setAcceleration(1000);
  stepperX.stop();
  stepperY.stop();
  stepperZ.stop();


  stepperX.setAcceleration(maxStepperAcc);
  stepperY.setAcceleration(maxStepperAcc);
  stepperZ.setAcceleration(maxStepperAcc);
  disableSteppers();
}
//---------------------------------------------------------
bool checkStopBtn() {
  if (digitalRead(stpBtnPin) == LOW) {
    Serial.println("$STOPBTN#");
    stopExecutionSB();
    spindleOFF();
    running = false;
    return true;
  }
  return false;
}

bool checkEndstops() {

  if (digitalRead(endStpPin) == LOW) {
    if (!ignoreStopExecution) {
      running = false;
      spindleOFF();
      stopExecution();
      return true;
    }
  }

  return false;
}
//------------------------------------------------------------
void printStatus() {
  Serial.print("< XmPos: ");
  Serial.print(machineAbsPosX);
  Serial.print(";YmPos: ");
  Serial.print(machineAbsPosY);
  Serial.print(";ZmPos: ");
  Serial.print(machineAbsPosZ);
  Serial.print(" >");
  Serial.println();
}
void enableSteppers() {
  digitalWrite(stepEnablePin, LOW);
}
void disableSteppers() {
  digitalWrite(stepEnablePin, HIGH);
}
// debuhg
void debugLn(String msg) {
  if (debug) {
    Serial.println(msg);
  }
}





