#!/bin/bash

# Copy the WorldSim Engine inside the WorldSim Unity Project.

cd "$(dirname "$0")"

# Change this to your needs
ENGINE=../../worldsim-engine/engine
TARGET=../Assets/WorldSim

mkdir -p $TARGET
cp -a -u -v $ENGINE/*.cs $TARGET/
