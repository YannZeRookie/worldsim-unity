#!/bin/bash

# Update the WorldSim Engine and copy it inside the WorldSim Unity Project.

cd "$(dirname "$0")"
SCRIPTS="$(pwd)"

# Change this if the Engine is somewhere else
ENGINE=../../worldsim-engine

cd $ENGINE
git pull

$SCRIPTS/copy-engine.sh
