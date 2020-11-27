#!/bin/bash
dotnet build; cd ./Core; electronize start /watch
