#!/bin/bash

# Copy the WorldSim Engine inside the WorldSim Unity Project.

cd "$(dirname "$0")"

ENGINE=../../worldsim-engine/engine
TARGET=../Assets/WorldSim

mkdir -p $TARGET
cp -a -v $ENGINE/*.cs $TARGET/
