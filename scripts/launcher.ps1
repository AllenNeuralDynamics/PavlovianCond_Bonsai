# $scriptPath = Split-Path -Parent $MyInvocation.MyCommand.Path
# Set-Location -Path (Split-Path -Parent $scriptPath)
param([string]$SchemaFolder=".\examples")

.\.venv\Scripts\Activate.ps1
&python $SchemaFolder\session.py
&python $SchemaFolder\rig.py
&python $SchemaFolder\task.py