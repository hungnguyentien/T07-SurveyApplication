@echo off
cd /d %~dp0
rmdir /s/q D:\Publish\SurveyApplicationApi

dotnet publish surveyapplication.api --output D:\Publish\SurveyApplicationApi

pause
