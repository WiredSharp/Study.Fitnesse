[CmdletBinding()]
Param(
    # fitnesse release
    [Parameter()][string] $Release="20190417"
    ,[Parameter()][int] $Port=9000
    )

$fitnesseJar="fitnesse-standalone.jar"

if (!(Test-Path $fitnesseJar)) {
    Write-Verbose "downloading fitnesse jar"
    Invoke-WebRequest -Uri "http://fitnesse.org/fitnesse-standalone.jar?responder=releaseDownload&release=$Release" -OutFile $fitnesseJar

}

if (!(Test-Path "fitsharp*")) {
    & nuget install fitsharp
}

& java -jar $fitnesseJar -p $Port