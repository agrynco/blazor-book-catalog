@echo off
for /l %%i in (1,1,25) do (
    curl -X GET http://localhost:5000/test
)
