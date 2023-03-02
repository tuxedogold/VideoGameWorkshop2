#this script will require elevated permissions to run
#make sure you are running this from an administrative command prompt


Set-ExecutionPolicy Bypass -Scope Process -Force; [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072; iex ((New-Object System.Net.WebClient).DownloadString('https://community.chocolatey.org/install.ps1'))
# install chocolatey, a prerequisite to installing any package



choco install vscode -Y

choco install dotnetcore -Y

choco install choco install dotnet-6.0-sdk
#^ add this to path! (C:\Program Files\dotnet)

choco install monogame -Y
#^ add this to path! 


#initialize the monogame template for your environment
dotnet new --install MonoGame.Templates.CSharp

Write-Host '---Completed Installation :) ---'

