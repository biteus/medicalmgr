#!/bin/bash

set -e
run_cmd="dotnet run --server.urls http://*:80"
#run_cmd="dotnet MedicalManager.dll --server.urls http://*:5000"
#"dotnet", "MedicalManager.dll"

until dotnet ef database update; do
>&2 echo "SQL Server is starting up"
sleep 1
done


>&2 echo "SQL Server is already up - So executing command to start the Web App.."
exec $run_cmd
