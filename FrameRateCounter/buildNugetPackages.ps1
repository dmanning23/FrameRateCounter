rm *.nupkg
nuget pack .\FrameRateCounter.nuspec -IncludeReferencedProjects -Prop Configuration=Release
nuget push *.nupkg