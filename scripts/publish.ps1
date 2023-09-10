# リリース用のファイルを発行します
# コマンドは、一つ上の階層で実行してください
# scripts\publish

$Date = Get-Date
$Year = $Date.Year.ToString()[-2..-1] -join ""
$Month = "{0:D2}" -f $Date.Month
$Day = "{0:D2}" -f $Date.Day

# dotnet clean .\Sokutatsu\Sokutatsu.csproj -c Release -r win10-x64
$publishDir = "Sokutatsu\Sokutatsu"
if(Test-Path $publishDir){
    Remove-Item -Recurse -Force $publishDir
}
dotnet publish .\Sokutatsu\Sokutatsu.csproj -c Release -r win10-x64 -p:PublishDir=$publishDir
Compress-Archive -Path README.md,LICENSE,".\Sokutatsu\$publishDir" -DestinationPath "Sokutatsu-$Year.$Month.$Day.zip" -Force