@echo off
cd /d %~dp0
rmdir /s/q D:\Publish\SurveyApplicationApi
rmdir /s/q D:\Publish\SurveyHangfire

dotnet publish surveyapplication.api --output D:\Publish\SurveyApplicationApi
dotnet publish surveyhangfire --output D:\Publish\SurveyHangfire

pause
