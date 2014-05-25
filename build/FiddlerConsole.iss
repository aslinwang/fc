;inno setup file

[Setup]
AppName=FiddlerConsole
AppPublisher=aslinwang, Inc.
AppPublisherURL=http://www.example.com/
AppVersion=0.1
DefaultDirName={code:getInstallDir}
DefaultGroupName=FiddlerConsole
Uninstallable=false
UsePreviousAppDir=no
DisableDirPage=true

[Files]
Source: "FiddlerConsole.dll"; DestDir: "{app}"

[Code]
//check App is running or not
function IsAppRunning(const FileName : string): Boolean;
var
    FSWbemLocator: Variant;
    FWMIService   : Variant;
    FWbemObjectSet: Variant;
begin
    Result := false;
    FSWbemLocator := CreateOleObject('WBEMScripting.SWBEMLocator');
    FWMIService := FSWbemLocator.ConnectServer('', 'root\CIMV2', '', '');
    FWbemObjectSet := FWMIService.ExecQuery(Format('SELECT Name FROM Win32_Process Where Name="%s"',[FileName]));
    Result := (FWbemObjectSet.Count > 0);
    FWbemObjectSet := Unassigned;
    FWMIService := Unassigned;
    FSWbemLocator := Unassigned;
end;

//check app is installed or not
function IsAppInstalled(const FileName : string): Boolean;
begin
  //MsgBox(AppPublisher, mbInformation, MB_OK);
  result := RegKeyExists(HKEY_LOCAL_MACHINE,'SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\' + FileName);
end;

//get app install path
function getAppPath(const FileName : string): string;
var V: string;
begin
  RegQueryStringValue(HKEY_LOCAL_MACHINE, 'SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\' + FileName, '', V);
  result := V;
end;

//get install dir (Fiddler2\Script)
function getInstallDir(dir:string): string;
var fiddlerPath:string ;
begin
  fiddlerPath := getAppPath('Fiddler.exe');
  //get app install dir
  StringChange(fiddlerPath, 'Fiddler.exe', 'Scripts');
  StringChange(fiddlerPath, '"', '');//the value in registry contains '"', it is invalid path
  result := fiddlerPath;
end;

function InitializeSetup(): Boolean;
var fileName:string ;
begin
  fileName := 'Fiddler.exe';
  if(IsAppRunning(fileName)) then
    begin
      MsgBox('Fiddler is running, please close it and retry', mbInformation, MB_OK);
      Result := false;
    end
  else if(not IsAppInstalled(fileName)) then
    begin
      MsgBox('Fiddler is not installed, please install it first', mbInformation, MB_OK);
      Result := false;
    end
  else
    begin
      Result := true;
    end
end;