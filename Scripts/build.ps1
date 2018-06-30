param (
    [string]$BuildTargets = 'Rebuild',
    [string]$WorkingDirectory = "$(Get-Location)",
    [string]$OutputPath = "$WorkingDirectory\bin\$($env:buildConfiguration)",
    [string]$MSBuildExe32 = '%ProgramFiles(x86)%\Microsoft Visual Studio\2017\BuildTools\MSBuild\15.0\Bin\MSBuild.exe',
    [string]$MSBuildExe64 = '%ProgramFiles%\Microsoft Visual Studio\2017\BuildTools\MSBuild\15.0\Bin\MSBuild.exe',
    [string]$Verbosity = 'minimal',
    [string]$GraphSchema
)

# Expand environment variables
$BuildTargets = ([System.Environment]::ExpandEnvironmentVariables($BuildTargets))
$WorkingDirectory = ([System.Environment]::ExpandEnvironmentVariables($WorkingDirectory))
$OutputPath = ([System.Environment]::ExpandEnvironmentVariables($OutputPath))
$MSBuildExe32 = ([System.Environment]::ExpandEnvironmentVariables($MSBuildExe32))
$MSBuildExe64 = ([System.Environment]::ExpandEnvironmentVariables($MSBuildExe64))

# Get the MSBuild.exe path
$msBuildExe = $MSBuildExe32
if (-Not (Test-Path $msBuildExe)) {
    $msBuildExe = $MSBuildExe64
}

# Install MSBuild.exe if it doesn't exist
if (-Not (Test-Path $msBuildExe)) {
    Write-Host "VS Build Tools could not be found.  If the following installation fails, you can install it from 'https://www.visualstudio.com/thank-you-downloading-visual-studio/?sku=BuildTools&rel=15'" -ForegroundColor Yellow
    $msBuildInstaller = ([System.Environment]::ExpandEnvironmentVariables("%TEMP%\vs_BuildTools.exe"))
    Invoke-WebRequest -OutFile "$msBuildInstaller" "https://download.visualstudio.microsoft.com/download/pr/11923325/e64d79b40219aea618ce2fe10ebd5f0d/vs_BuildTools.exe"
    $process = Start-Process -FilePath "$msBuildInstaller" -Verb RunAs -Wait -PassThru -ArgumentList "--add Microsoft.VisualStudio.Workload.MSBuildTools --add Microsoft.Net.Component.4.6.2.TargetingPack --add Microsoft.VisualStudio.Component.NuGet.BuildTools --add Microsoft.VisualStudio.Workload.WebBuildTools --norestart --quiet --force"
    if ($process.ExitCode -eq 0) {
      Write-Host "Done installing VS Build Tools" -ForegroundColor Green

        # Double check that MSBuild.exe now exists
        $msBuildExe = $MSBuildExe32
        if (-Not (Test-Path $msBuildExe)) {
            $msBuildExe = $MSBuildExe64
        }
        if (-Not (Test-Path $msBuildExe)) {
            Write-Host "MSBuild was not found at '$msBuildExe'" -ForegroundColor Red
        }
    } else {
        # Failed to install - print an error on the command line
        throw "Failed to install VS Build Tools. Exit code: $($process.ExitCode)"
    }
}

# Compile the MSBuild arguments
$MSBuildArguments = @(
    "/p:Configuration=$($env:buildConfiguration)",
    "/p:OutputPath=`"$OutputPath`"",
    "/t:$BuildTargets",
    "/p:UseSharedCompilation=false",
    "/nr:false",
    "/v:$Verbosity",
    "/ignore:.sln"
)

# Select the default graph schema if one was not provided
if ($GraphSchema)
{
    # Add the schema path to the MSBuild arguments if it is a valid path
    if (Test-Path $GraphSchema)
    {
        $MSBuildArguments += "/p:GraphSchemaPath=`"$GraphSchema`""
        Write-Host "MSBuild arguments: " -f Magenta
        $MSBuildArguments | ForEach-Object {
            Write-Host $_ -f Magenta
        }
    } else {
        throw "Unable to find Graph schema at '$GraphSchema' - provide a valid path to a schema file"
    }
}

# Run MSBuild in the given working directory
Push-Location $WorkingDirectory
try {
    & $msBuildExe $MSBuildArguments
    if (-Not $?)
    {
        throw "MSBuild exited with error code '$LastExitCode'"
    }
} finally {
    Pop-Location
}
