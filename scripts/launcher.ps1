param([string]$SchemaFolder="examples")

$scriptPath = Split-Path -Parent $MyInvocation.MyCommand.Path
Set-Location -Path (Split-Path -Parent $scriptPath)

. $scriptPath\..\.venv\Scripts\Activate.ps1
&python $scriptPath\..\$SchemaFolder\session.py
&python $scriptPath\..\$SchemaFolder\rig.py
&python $scriptPath\..\$SchemaFolder\task.py

& $scriptPath\..\.bonsai\bonsai.exe $scriptPath\..\src\main.bonsai -p SessionPath=$scriptPath\..\local\AindBehaviorSessionModel.json -p RigPath=$scriptPath\..\local\AindPavlovianConditioningRig.json -p TaskLogicPath=$scriptPath\..\local\AindPavlovianConditioningTaskLogic.json 